using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;
using static UIManager;


public class VoteManager : MonoBehaviourPun
{
	public static VoteManager VM;
	void Awake() => VM = this;
	int callActor, voteTimer;
	bool isAllVoted;
	

	public void VoteInit(int _callActor)
	{
		// 변수 초기화
		callActor = _callActor;
		isAllVoted = false;
		foreach (PlayerScript player in NM.Players)
			player.VoteColorIndex = -2;
		voteTimer = NM.VoteTimer;

		SortPlayer();
		ChatManager.CM.ChatClear();
		StopCoroutine(VoteTimerCo());
		StartCoroutine(VoteTimerCo());

		// 패널 초기화
		for (int i = 0; i < UM.VotePanels.Length; i++)
		{
			bool isExist = i < NM.Players.Count;
			Transform CurVotePanel = UM.VotePanels[i];
			CurVotePanel.gameObject.SetActive(isExist);

			if (isExist)
			{
				PlayerScript CurPlayer = NM.Players[i];
				CurVotePanel.GetChild(0).GetComponent<Image>().color = NM.GetColor(CurPlayer.colorIndex);
				CurVotePanel.GetChild(1).gameObject.SetActive(CurPlayer.actor == callActor);
				Text NickText = CurVotePanel.GetChild(3).GetComponent<Text>();
				NickText.text = CurPlayer.nick;
				NickText.color = NM.MyPlayer.isImposter && CurPlayer.isImposter ? Color.red : Color.white;

				// 죽은 사람은 비활성화
				CurVotePanel.GetComponent<CanvasGroup>().alpha = CurPlayer.isDie ? 0.7f : 1;
				CurVotePanel.GetChild(5).GetComponent<Toggle>().interactable = !CurPlayer.isDie;
				CurVotePanel.GetChild(6).gameObject.SetActive(CurPlayer.isDie);

				// 투표받은 리스트 초기화
				CurPlayer.PV.RPC("VotedColorsClearRPC", RpcTarget.All);
			}
			else
				CurVotePanel.GetChild(1).gameObject.SetActive(false);

			CurVotePanel.GetChild(4).gameObject.SetActive(false);
			CurVotePanel.GetChild(5).GetComponent<Toggle>().isOn = false;
			for (int j = 0; j < CurVotePanel.GetChild(7).childCount; j++)
				Destroy(CurVotePanel.GetChild(7).GetChild(j).gameObject);
		}

		for (int i = 0; i < UM.SkipVoteResultGrid.transform.childCount; i++)
			Destroy(UM.SkipVoteResultGrid.transform.GetChild(i).gameObject);

		UM.SkipVoteToggle.isOn = false;
		UM.CancelVoteToggle.isOn = true;



		ActiveToggles(true);



		// 자기가 죽은 상태이면 토글 모두 비활성화
		if (NM.MyPlayer.isDie)
		{
			for (int i = 0; i < UM.VotePanels.Length; i++)
				UM.VotePanels[i].GetChild(5).GetComponent<Toggle>().interactable = false;

			UM.CancelVoteToggle.interactable = false;
			UM.SkipVoteToggle.interactable = false;
		}
	}


	public void ToggleChanged(int toggle)
	{
		if (toggle < 0) NM.MyPlayer.VoteColorIndex = toggle; // -1 투표 건너뛰기, -2 모두취소
		else NM.MyPlayer.VoteColorIndex = NM.Players[toggle].colorIndex;
		photonView.RPC("VoteUpdateRPC", RpcTarget.All);
	}


	void SortPlayer()
	{
		NM.Players.Sort((p1, p2) => p1.colorIndex.CompareTo(p2.colorIndex));
		NM.Players.Sort((p1, p2) => p1.isDie.CompareTo(p2.isDie));
	}

	void ActiveToggles(bool b)
	{
		for (int i = 0; i < UM.VotePanels.Length; i++)
			UM.VotePanels[i].GetChild(5).gameObject.SetActive(b);

		UM.CancelVoteToggle.gameObject.SetActive(b);
		UM.SkipVoteToggle.gameObject.SetActive(b);
	}


	[PunRPC]
	void VoteUpdateRPC()
	{
		SortPlayer();


		// 패널 업데이트
		for (int i = 0; i < UM.VotePanels.Length; i++)
		{
			bool isExist = i < NM.Players.Count;
			Transform CurVotePanel = UM.VotePanels[i];
			CurVotePanel.gameObject.SetActive(isExist);

			if (isExist)
			{
				PlayerScript CurPlayer = NM.Players[i];
				CurVotePanel.GetChild(0).GetComponent<Image>().color = NM.GetColor(CurPlayer.colorIndex);
				CurVotePanel.GetChild(1).gameObject.SetActive(CurPlayer.actor == callActor);
				Text NickText = CurVotePanel.GetChild(3).GetComponent<Text>();
				NickText.text = CurPlayer.nick;
				NickText.color = NM.MyPlayer.isImposter && CurPlayer.isImposter ? Color.red : Color.white;
				CurVotePanel.GetChild(4).gameObject.SetActive(CurPlayer.VoteColorIndex != -2);
			}
			else
			{
				CurVotePanel.GetChild(1).gameObject.SetActive(false);
				CurVotePanel.GetChild(4).gameObject.SetActive(false);
			}
		}

		AllVotedCheck();
	}


	void AllVotedCheck()
	{
		int aliveCount = 0;
		int votedCount = 0;
		for (int i = 0; i < NM.Players.Count; i++)
		{
			var CurPlayer = NM.Players[i];
			if (!CurPlayer.isDie)
			{
				++aliveCount;
				if (CurPlayer.VoteColorIndex != -2) ++votedCount;
			}
		}

		if (!isAllVoted && aliveCount == votedCount)
		{
			isAllVoted = true;
			ActiveToggles(false);
			Invoke("ShowVoteResultIcon", 0.5f);
		}
	}


	void ShowVoteResultIcon() 
	{
		// 모든 클라이언트가 결과 실행
		
		List<int> SkipVotedColors = new List<int>();
		for (int i = 0; i < NM.Players.Count; i++)
		{
			var CurPlayer = NM.Players[i];
			if (CurPlayer.isDie) continue;
			if (CurPlayer.VoteColorIndex != -1) // CurPlayer가 사람을 투표했다면
				NM.Players.Find(x => x.colorIndex == CurPlayer.VoteColorIndex)?.VotedColorsAdd(CurPlayer.colorIndex);
			else // CurPlayer가 스킵했다면
				SkipVotedColors.Add(CurPlayer.colorIndex);
		}


		// 투표 작은 아이콘 표시
		for (int i = 0; i < NM.Players.Count; i++) 
		{
			var CurPlayer = NM.Players[i];
			if (CurPlayer.isDie) continue;

			for (int j = 0; j < CurPlayer.VotedColors.Count; j++)
			{
				var VoteResultImage = Instantiate(UM.VoteResultImage, UM.VotePanels[i].GetChild(7));
				VoteResultImage.GetComponent<Image>().color = NM.GetColor(CurPlayer.VotedColors[j]);
			}
		}

		for (int i = 0; i < SkipVotedColors.Count; i++)
		{
			var VoteResultImage = Instantiate(UM.VoteResultImage, UM.SkipVoteResultGrid.transform);
			VoteResultImage.GetComponent<Image>().color = NM.GetColor(SkipVotedColors[i]);
		}

		Invoke("VoteResultLogic", 3f);
	}


	void VoteResultLogic()
	{
		if (!PhotonNetwork.IsMasterClient) return;

		int maxCount = 0;
		for (int i = 0; i < NM.Players.Count; i++)
		{
			var CurPlayer = NM.Players[i];
			if (CurPlayer.isDie) continue;

			if (maxCount < CurPlayer.VotedColors.Count) maxCount = CurPlayer.VotedColors.Count;
		}

		int overlapCount = 0;
		int maxColorIndex = -10;
		for (int i = 0; i < NM.Players.Count; i++)
		{
			var CurPlayer = NM.Players[i];
			if (CurPlayer.isDie) continue;

			if (maxCount == CurPlayer.VotedColors.Count)
			{
				++overlapCount;
				maxColorIndex = CurPlayer.colorIndex;
			}
		}

		bool isKick = maxCount > 0 && overlapCount == 1;
		photonView.RPC("ReturnToGameRPC", RpcTarget.AllViaServer, isKick, maxColorIndex);
	}

	[PunRPC]
	void ReturnToGameRPC(bool isKick, int maxColorIndex) 
	{
		StartCoroutine(ReturnToGameCo(isKick, maxColorIndex));
	}

	IEnumerator ReturnToGameCo(bool isKick, int maxColorIndex) 
	{
		if (!NM.VotePanel.activeInHierarchy) yield break;


		if (isKick) // maxColorIndex가 퇴출당한다
		{
			NM.ShowPanel(NM.KickPanel);
			var KickPanel = NM.KickPanel.transform;
			KickPanel.GetChild(0).GetComponent<Image>().color = NM.GetColor(maxColorIndex);

			var targetPlayer = NM.Players.Find(x => x.colorIndex == maxColorIndex);
			string impoCrew = targetPlayer.isImposter ? "<color=red>임포스터</color>였" : "크루원이었";
			KickPanel.GetChild(2).GetComponent<Text>().text = $"{targetPlayer.nick}은 {impoCrew}습니다";
			targetPlayer.PV.RPC("SetVotedDie", RpcTarget.All);
		}
		else // 아무도 퇴출당하지 않음
			NM.ShowPanel(NM.NoOneKickPanel);

		NM.MyPlayer.SetPos(NM.SpawnPoint.position);

		ChatManager.CM.photonView.RPC("ChatClearRPC", RpcTarget.AllViaServer, false);
		yield return new WaitForSeconds(3);
		NM.ShowPanel(NM.GamePanel);
		NM.WinCheck();
		StartCoroutine(UM.KillCo());
		StartCoroutine(UM.EnergencyCo());
		StopCoroutine(VoteTimerCo());
	}

	IEnumerator VoteTimerCo() 
	{
		int _voteTimer = voteTimer;
		for (int i = _voteTimer; i >= 0; i--)
		{
			voteTimer = i;
			UM.VoteTimerText.text = $"{voteTimer}초후 투표 건너뛰기";
			yield return new WaitForSeconds(1);
		}

		// 투표 안한 사람은 강제로 -1선택
		if (NM.MyPlayer.VoteColorIndex == -2) 
		{
			UM.SkipVoteToggle.isOn = true;
		}
	}


}
