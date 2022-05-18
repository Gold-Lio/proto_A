using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using static UIManager;
using static NetworkManager;

public enum State
{
    Idle, Walk
}

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    private float speed;
    Vector2 input;

    //public Transform playerTransform, Canvas;
    //public Text nickText;

    public State state;

    public bool isWalk, isMove, isImposter, isKillable, isDie;

    //public int actor, colorIndex;

    private Animation playerAnim;

    [HideInInspector] public PhotonView PV;
    [HideInInspector] public string nick;

    private void Start()
    {
        PV = photonView;
      //  actor = PV.Owner.ActorNumber;
        nick = PV.Owner.NickName;
      //  SetNick();
        //  NM.Players.Add(this);
        NM.SortPlayers();
        isMove = true;
        //  StartCoroutine(StateCo());

        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animation>();
    }

    private void Update()
    {
        if (!PV.IsMine) return;
        else if (PV.IsMine)
        {
            isMove = true;

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            rb.velocity = input * speed;
            isWalk = rb.velocity != Vector2.zero;

            //¿òÁ÷ÀÓÀÇ Æ÷Åæºä
            PV.RPC("AnimSprites", RpcTarget.All, isWalk, input);
        }

        if (NM.isGameStart)
        {
            Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);
        }
        NM.PointLight2D.transform.position = transform.position + new Vector3(0, 0, 10); input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


    }


    //void SetNick()
    //{
    //    nickText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
    //}

}
