using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;
using UnityEngine.UI;
public class InteractionScript : MonoBehaviourPun
{
	private PlayerScript PS;
	public static InteractionScript IS;
	public enum Type { Customize, Mission, Worthy , EndGameChenck };
	public Type type;
	GameObject Line;
	public int curInteractionNum;
	public bool isCleanerWin;


	void Start()
    {		
		Line = transform.GetChild(0).gameObject;

		UM.SetInteractionBtn0(0, false);
		UM.SetInteractionBtn1(0, false);
		UM.SetInteractionBtn2(5, false);

		PS = GetComponent<PlayerScript>();
    }


    [PunRPC]
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine)
		{
			if (type == Type.Customize)
			{
				Line.SetActive(true);
				UM.SetInteractionBtn0(1, true);
			}

			else if (type == Type.Mission)
			{
				UM.curInteractionNum = curInteractionNum;
				Line.SetActive(true);
				UM.SetInteractionBtn1(0, true);
			}

			else if (type == Type.Worthy) // 현재 보물상자는 미션진행 불가능   
			{
				Line.SetActive(true);
				UM.SetInteractionBtn1(0, true);
			}


			//EndGame 기능

			if (type == Type.EndGameChenck)
			{
				if(col.GetComponent<PlayerScript>().isImposter)  
				{
					Debug.Log("살인마는 방 밖으로 나갈 수 없읍니다.");
					UM.broadCastText.text = "살인마는 방 밖으로 나갈 수 없습니다. 남은 사람들을 처리하세요.";
					StartCoroutine(Wait()); //3초 뒤에 꺼지는 코루틴
				}
				else if(!col.GetComponent<PlayerScript>().isImposter)
				{
					isCleanerWin = true;
					Debug.Log("탐험로봇");
					StartCoroutine(WincheckCo());
				}
			}
		}
	}

    [PunRPC]
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine) 
		{
			if (type == Type.Customize)
			{
				Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}

			else if (type == Type.Mission)
			{
				Line.SetActive(false);
				UM.SetInteractionBtn1(0, false);
			}

			else if (type == Type.Worthy)  // 현재 보물상자는 미션진행 불가능   
			{
				Line.SetActive(false);
				UM.SetInteractionBtn1(0, false);
			}
			//여기서 캐릭터가 일정 수준 떨어질 경우.  -모든 미션창 취소되고 처음 으로 돌아가도록. 
		}
	}

	IEnumerator Wait()
    {
		yield return new WaitForSeconds(3.0f);
		UM.broadCastText.text = "";
    }

    [PunRPC]
	IEnumerator WincheckCo()
    {
		NM.Winner(true);
		yield return null;
	}
}
