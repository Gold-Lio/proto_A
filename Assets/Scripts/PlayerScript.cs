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
	public SpriteRenderer[] CharacterSR;
	public SpriteRenderer SR;

	public Transform Character, Canvas ;
	public Text NickText;

	public enum State { Idle, Walk };
	public State state;
	public bool isWalk, isMove, isImposter, isKillable, isDie;
	public int actor, colorIndex;
	public float speed; //기본 40
	public PlayerScript KillTargetPlayer;
	public int targetDeadColorIndex;

	[HideInInspector] public PhotonView PV;
	[HideInInspector] public string nick;
	Vector2 input;
	bool facingRight;


	public GameObject punchGo;
	public Animator punchAnim;
	//public ParticleSystem punchEffect;
	//public AudioClip audioClip;

	void Start()
	{
		PV = photonView;
		actor = PV.Owner.ActorNumber;
		nick = PV.Owner.NickName;
		SetNick();
		NM.Players.Add(this);
		NM.SortPlayers();
		isMove = true;
		//StartCoroutine(StateCo());
	}

	//IEnumerator StateCo()
	//{
	//	while (true) yield return StartCoroutine(state.ToString());
	//}

	//void OnDestroy()
	//{
	//	NM.Players.Remove(this);
	//	NM.SortPlayers();
	//}

	void SetNick()
	{
		NickText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
	}

	void Update()
	{
		if (PV.IsMine)
        {
			Move();
			PV.RPC("Filp", RpcTarget.AllBuffered, input);
			NM.PointLight2D.transform.position = transform.position + new Vector3(0, 0, 10);
		}

	}

    [PunRPC]
    public void Move()
    {
		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
		RB.MovePosition(RB.position + (input * speed * Time.deltaTime));
	}

    [PunRPC]
    public void Filp(Vector2 input)
    {
		if (input.x < 0 && !facingRight)
		{
			transform.localScale = new Vector2(1, 1); // left flip.
		}

		if (input.x > 0 && !facingRight)
		{
			transform.localScale = new Vector2(-1, 1); // left flip.
		}
	}



    public void SetPos(Vector3 target)
	{
		transform.position = target;
	}


	//[PunRPC]
	//void AnimSprites(bool _isWalk, Vector2 _input)
	//{
	//	if (_isWalk)
	//	{
	//		state = State.Walk;

	//		if (_input.x == 0) return;
	//		if (_input.x < 0)
	//		{
	//			Character.localScale = Vector3.one;
	//			if (Ghost.gameObject.activeInHierarchy) Ghost.localScale = Vector3.one;
	//		}
	//		else
	//		{
	//			Character.localScale = new Vector3(-1, 1, 1);
	//			if (Ghost.gameObject.activeInHierarchy) Ghost.localScale = new Vector3(-1, 1, 1);
	//		}
	//	}
	//	else
	//	{
	//		state = State.Idle;
	//	}
	//}

	//void ShowAnim(int index)
	//{
	//	for (int i = 0; i < Anims.Length; i++)
	//		Anims[i].SetActive(index == i);
	//}

	//IEnumerator Idle()
	//{
	//	ShowAnim(0);
	//	yield return new WaitForSeconds(0.1f);
	//}

	//IEnumerator Walk()
	//{
	//	ShowAnim(0);
	//	yield return new WaitForSeconds(0.15f);
	//	ShowAnim(1);
	//	yield return new WaitForSeconds(0.15f);
	//}

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


	public void SetMission()
	{
		if (!PV.IsMine) return;
		if (isImposter) return;

		List<int> GachaList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		for (int i = 0; i < 4; i++)
		{
			int rand = Random.Range(0, GachaList.Count);
			NM.Interactions[GachaList[rand]].SetActive(true);
			GachaList.RemoveAt(rand);
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
		if (!PV.IsMine || !isKillable || col.GetComponent<PlayerScript>().isDie) return;

		//살인자
		if (isImposter && col.GetComponent<PlayerScript>())
		{
			UM.SetInteractionBtn1(5, true);
			KillTargetPlayer = col.GetComponent<PlayerScript>();
		}

		//일반 
		else if (!isImposter && col.GetComponent<PlayerScript>())
		{
			UM.SetInteractionBtn1(5, true);
			KillTargetPlayer = col.GetComponent<PlayerScript>();
		}

		//if(col.GetComponent<!!!>) 아이템스크립트를 가지고 있다면 setinteractionBtn의 2의 6번이 켜져야한다. 



	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (!col.CompareTag("Player") || !NM.isGameStart) return;
		if (!PV.IsMine /*|| !isImposter */ || !isKillable || col.GetComponent<PlayerScript>().isDie) return;

		if (col.GetComponent<PlayerScript>())
		{
			UM.SetInteractionBtn1(5, false);
			KillTargetPlayer = null;
		}
	}

	[PunRPC]
	public void Punch()  // 펀치 함수. 
	{
		punchGo.SetActive(true);
		punchAnim.SetTrigger("IsPunch");
		StartCoroutine(punchCo());
		// 죽이기 성공
		StartCoroutine(UM.PunchCoolCo());

		//KillTargetPlayer.GetComponent<PhotonView>().RPC("SetDie", RpcTarget.AllViaServer, true, colorIndex, KillTargetPlayer.colorIndex);
		//Vector3 TargetPos = KillTargetPlayer.transform.position;
		//transform.position = TargetPos;

		//GameObject CurDeadBody = PhotonNetwork.Instantiate("DeadBody", TargetPos, Quaternion.identity);
		//CurDeadBody.GetComponent<PhotonView>().RPC("SpawnBody", RpcTarget.AllViaServer, KillTargetPlayer.colorIndex, Random.Range(0, 2));
	}

	IEnumerator punchCo()
    {
		yield return new WaitForSeconds(0.5f);
		punchGo.SetActive(false);
    }

}


//	[PunRPC] //인벤토리를 쏟아낼 함수
	//void SetDie(bool b, int _killerColorIndex, int _deadBodyColorIndex) 
	//{
	//	isDie = b;

	//	transform.GetChild(0).gameObject.SetActive(false);
	//	transform.GetChild(1).gameObject.SetActive(false);

	//	if (PV.IsMine) 
	//	{
	//		StartCoroutine(UM.DieCo(_killerColorIndex, _deadBodyColorIndex));

	//		transform.GetChild(1).gameObject.SetActive(true); //유령을 스폰한다. 
	//		transform.GetChild(2).gameObject.SetActive(true);
	//		Physics2D.IgnoreLayerCollision(8, 9);
	//		PV.RPC("SetGhostColor", RpcTarget.AllViaServer, colorIndex);
	//		NM.GetComponent<PhotonView>().RPC("ShowGhostRPC", RpcTarget.AllViaServer);
	//	}
	//}

