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


    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public SpriteRenderer[] CharacterSR;

    public Transform Character, Canvas;
    public Text NickText;

    public bool isWalk, isMove, isImposter, isKillable, isDie;
    public int actor, colorIndex;
    public float speed; //기본 40
    public PlayerScript KillTargetPlayer;
    public int targetDeadColorIndex;

    [HideInInspector] public PhotonView PV;
    [HideInInspector] public string nick;
    Vector2 input;
    bool facingRight;
    bool isHurt;

    Vector2 playerDir;
    Vector3 curPos;

    public Animator punchAnim;

    RaycastHit hitInfo;
    public LayerMask layMask;

    private void Awake()
    {
        PS = this;
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

    [PunRPC]
    void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }

        //Debug.DrawRay(transform.position, new Vector3(1, 0, 0), Color.red);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, layerMask("Item");     
        //       RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), 0.9f, LayerMask.GetMask("Item"));

        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 0.9f, new Color(0, 1, 0));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0) * 5f);

        if(hit.collider.CompareTag("Item"))
        {
            Debug.Log(hit.collider.name);
        }

        //if (hit.collider != null)
        //{
        //    Debug.Log(hit.collider.name);
        //}
        //걸렸을때ㅡ 어떻게 판정할거야?

        float inputX = Input.GetAxisRaw("Horizontal");

        float inputY = Input.GetAxisRaw("Vertical");

        input = new Vector2(inputX, inputY);
        input *= speed;
        RB.velocity = input.normalized * speed;

        if (PV.IsMine)
        {
            if (inputX != 0)
            {
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, inputX);
            }
            NM.PointLight2D.transform.position = transform.position + new Vector3(0, 0, 10);
        }
        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    [PunRPC]
    void FlipXRPC(float axis) => SR.flipX = axis == 1;

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

    void CheckItem()
    {

    }
}
