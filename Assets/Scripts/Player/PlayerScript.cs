﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static NetworkManager;
using static UIManager;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerScript PS;

    //[SerializeField]
    //private InventorySlot theInventory;

    private Joystick joystick;
    public float range; // 습득 가능한 최대 거리.

    // private bool pickupActivated = false; // 습득 가능할 시 true.

    private RaycastHit hitInfo; // 충돌체 정보 저장.

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정.
    [SerializeField]
    private LayerMask layerMask;

    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public SpriteRenderer[] CharacterSR;

    public Transform Character, Canvas;
    public Text NickText;

    public bool isWalk, isMove, isImposter, ispunch, ishited, isDie;
    public int actor, colorIndex;
    public float speed; //기본 40
    public PlayerScript KillTargetPlayer;
    public int targetDeadColorIndex;

    [HideInInspector] public PhotonView PV;
    [HideInInspector] public string nick;
    Vector2 input;
    bool facingRight;

    Vector2 playerDir;
    Vector3 curPos;


    private void Awake()
    {
        PS = this;
        joystick = GameObject.FindObjectOfType<Joystick>();
    }

    void Start()
    {
        PV = photonView;
        actor = PV.Owner.ActorNumber;
        nick = PV.Owner.NickName;
        SetNick();
        NM.Players.Add(this);
        NM.SortPlayers();
        isMove = true;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    void SetNick()
    {
        NickText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
    }

    private void Update()
    {

    }


    [PunRPC]
    void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (!ishited)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            input = new Vector2(inputX, inputY);
            input *= speed;
            RB.velocity = input.normalized * speed;

            if (inputX != 0)
            {
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, inputX);
            }


            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                MoveControl();

            NM.PointLight2D.transform.position = transform.position + new Vector3(0, 0, 10);
        }

        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }


    [PunRPC]
    void FlipXRPC(float axis) => SR.flipX = axis == 1;

    private void MoveControl()
    {
        Vector3 upMovement = Vector3.up * speed * Time.deltaTime * joystick.Vertical;
        Vector3 rightMovement = Vector3.right * speed * Time.deltaTime * joystick.Horizontal;
        transform.position += upMovement;
        transform.position += rightMovement;
    }

    public void SetPos(Vector3 target)
    {
        transform.position = target;
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

    [PunRPC]
    public void Punch()  // 펀치 함수. 
    {
        PhotonNetwork.Instantiate("Glove", transform.position + new Vector3(SR.flipX ? 9f : -9f, 0f, -1f), Quaternion.Euler(0, 0, -180))
                         .GetComponent<PhotonView>().RPC("DirRPC", RpcTarget.All, SR.flipX ? 1 : -1);

        StartCoroutine(UM.PunchCoolCo());

        //KillTargetPlayer.GetComponent<PhotonView>().RPC("Punch", RpcTarget.AllViaServer, true);

        //KillTargetPlayer.GetComponent<PhotonView>().RPC("SetDie", RpcTarget.AllViaServer, true, colorIndex, KillTargetPlayer.colorIndex);
        //Vector3 TargetPos = KillTargetPlayer.transform.position;
        //transform.position = TargetPos;

        //GameObject CurDeadBody = PhotonNetwork.Instantiate("DeadBody", TargetPos, Quaternion.identity);
        //CurDeadBody.GetComponent<PhotonView>().RPC("SpawnBody", RpcTarget.AllViaServer, KillTargetPlayer.colorIndex, Random.Range(0, 2));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }

    public void CheckItem()
    {
        //if (hitInfo.transform.tag == "Item")
        //{
        Debug.Log("들어옴");
        //    theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
        //    Destroy(hitInfo.transform.gameObject);
    }


}
