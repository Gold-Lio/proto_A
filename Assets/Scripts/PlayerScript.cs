using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static NetworkManager;
using static UIManager;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    
	public Rigidbody2D RB;
	public GameObject[] Anims;
	public SpriteRenderer[] CharacterSR;
	public Transform Character, Canvas, Ghost;
	public Text NickText;
	public enum State { Idle, Walk };
	public State state;
	public bool isWalk, isMove, isImposter, isKillable, isDie;
	public int actor, colorIndex;
	public float speed; //기본 40
	public PlayerScript KillTargetPlayer;
	public int targetDeadColorIndex;

	//[SerializeField] int _voteColorIndex; // 투표한 사람 색
	//public int VoteColorIndex { get => _voteColorIndex; set => PV.RPC("VoteColorIndexRPC", RpcTarget.AllBuffered, value); }
	//[PunRPC] void VoteColorIndexRPC(int value) { _voteColorIndex = value; }

	//public List<int> VotedColors = new List<int>();

	[HideInInspector] public PhotonView PV;
	[HideInInspector] public string nick;
	Vector2 input;

	void Start()
	{
		PV = photonView;
		actor = PV.Owner.ActorNumber;
		nick = PV.Owner.NickName;
		SetNick();
		NM.Players.Add(this);
		NM.SortPlayers();
		isMove = true;
		StartCoroutine(StateCo());
	}

	IEnumerator StateCo() 
	{
		while (true) yield return StartCoroutine(state.ToString()); 
	}

	void OnDestroy()
	{
		NM.Players.Remove(this);
		NM.SortPlayers();
	}

	void SetNick() 
	{
		NickText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
	}
	void Update()
    {
		if (!PV.IsMine) return;

		if (isMove) 
		{
			input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
			RB.velocity = input * speed;
			isWalk = RB.velocity != Vector2.zero;
			PV.RPC("AnimSprites", RpcTarget.All, isWalk, input);
		}

		if (NM.isGameStart) 
		{
			Camera.main.transform.position = transform.position + new Vector3(0,0,-10);
		}

		NM.PointLight2D.transform.position = transform.position + new Vector3(0,0,10);
	}

	public void SetPos(Vector3 target) 
	{
		transform.position = target;
	}

	[PunRPC]
	void AnimSprites(bool _isWalk, Vector2 _input) 
	{
		if (_isWalk)
		{
			state = State.Walk;

			if (_input.x == 0) return;
			if (_input.x < 0) 
			{
				Character.localScale = Vector3.one;
				if (Ghost.gameObject.activeInHierarchy) Ghost.localScale = Vector3.one;
			}
			else 
			{
				Character.localScale = new Vector3(-1, 1, 1);
				if (Ghost.gameObject.activeInHierarchy) Ghost.localScale = new Vector3(-1, 1, 1);
			}
		}
		else 
		{
			state = State.Idle;
		}
	}

	void ShowAnim(int index)
	{
		for (int i = 0; i < Anims.Length; i++)
			Anims[i].SetActive(index == i);
	}
	IEnumerator Idle() 
	{
		ShowAnim(0);
		yield return new WaitForSeconds(0.1f);
	}
	IEnumerator Walk() 
	{
		ShowAnim(0);
		yield return new WaitForSeconds(0.15f);
		ShowAnim(1);
		yield return new WaitForSeconds(0.15f);
	}

	[PunRPC]
	public void SetColor(int _colorIndex) 
	{
		CharacterSR[0].color = UM.colors[_colorIndex];
		CharacterSR[1].color = UM.colors[_colorIndex];
		colorIndex = _colorIndex;
	}

	[PunRPC]
	void SetImpoCrew(bool _isImposter) 
	{
		isImposter = _isImposter;
	}

	public void SetNickColor() 
	{
		if (!isImposter) return;

		for (int i = 0; i < NM.Players.Count; i++)
		{
			if (NM.Players[i].isImposter) NM.Players[i].NickText.color = Color.red;
		}
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if (!col.gameObject.CompareTag("Player")) return;
		Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), col.gameObject.GetComponent<CapsuleCollider2D>());
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (!col.CompareTag("Player") || !NM.isGameStart) return;
		if (!PV.IsMine /*|| !isImposter */ || !isKillable || col.GetComponent<PlayerScript>().isDie) return;

		//살인자가 주체_    user또한 주체가 되어야한다. 
		if (col.GetComponent<PlayerScript>()) 
		{
			UM.SetInteractionBtn2(5, true);
			KillTargetPlayer = col.GetComponent<PlayerScript>();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		
		if (!col.CompareTag("Player") || !NM.isGameStart) return;
		if (!PV.IsMine /*|| !isImposter */ || !isKillable || col.GetComponent<PlayerScript>().isDie) return;

		if (col.GetComponent<PlayerScript>())
		{
			UM.SetInteractionBtn2(5, false);
			KillTargetPlayer = null;
		}
	}


	public void Kill() 
	{
		// 죽이기 성공
		StartCoroutine(UM.KillCo());
		KillTargetPlayer.GetComponent<PhotonView>().RPC("SetDie", RpcTarget.AllViaServer, true, colorIndex, KillTargetPlayer.colorIndex);
		Vector3 TargetPos = KillTargetPlayer.transform.position;
		transform.position = TargetPos;
		
		GameObject CurDeadBody = PhotonNetwork.Instantiate("DeadBody", TargetPos, Quaternion.identity);
		CurDeadBody.GetComponent<PhotonView>().RPC("SpawnBody", RpcTarget.AllViaServer, KillTargetPlayer.colorIndex, Random.Range(0, 2));
	}

	[PunRPC]
	void SetDie(bool b, int _killerColorIndex, int _deadBodyColorIndex) 
	{
		isDie = b;

		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(false);

		if (PV.IsMine) 
		{
			StartCoroutine(UM.DieCo(_killerColorIndex, _deadBodyColorIndex));
			transform.GetChild(1).gameObject.SetActive(true);
			transform.GetChild(2).gameObject.SetActive(true);
			Physics2D.IgnoreLayerCollision(8, 9);
			PV.RPC("SetGhostColor", RpcTarget.AllViaServer, colorIndex);
			NM.GetComponent<PhotonView>().RPC("ShowGhostRPC", RpcTarget.AllViaServer);
		}
	}

	[PunRPC]
	void SetGhostColor(int colorIndex) 
	{
		Color color = UM.colors[colorIndex];
		Ghost.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);
	}

}
