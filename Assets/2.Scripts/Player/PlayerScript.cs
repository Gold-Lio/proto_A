using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static NetworkManager;
using static UIManager;
using Random = UnityEngine.Random;
using UnityEngine.Experimental.Rendering.Universal;
using Photon.Voice.Unity.Demos.DemoVoiceUI;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerScript PS;
    //public Inventory inventory;
    
    public Rigidbody2D RB; 
    public SpriteRenderer SR;  //  이SR 부분들. Flip에 들어가는  SR, 색깔 구분에 들어가는  SR. 
    public SpriteRenderer[] CharacterSR;
    public Light2D playerStaffLight2D;

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

    Vector2 curScale;
    Vector2 playerDir;
    Vector3 curPos;
    private IPunObservable _punObservableImplementation; 

    public GameObject playerCanvasGo;
    public Animator anim;

    //public AudioSource walkAudio;

    // 아이템 드랍 거리
    private float dropRange = 2.0f;
    // 플레이어 인풋 액션 추가
    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        PS = this;
        anim =  gameObject.GetComponent<Animator>();

        // 플레이어 인풋 액션 추가 (드랍, 인벤토리 onoff)
        playerInputAction = new PlayerInputAction();
        //walkAudio = GetComponent<AudioSource>();
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
        facingRight = true;
    }


    /// <summary>
    ///현재 개체 소유자를 가져오는 도우미 방법입니다.
    /// Helper method for getting the current object owner.
    /// </summary>
    public PhotonView GetView()
    {
        return this.photonView;
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
        if (!PV.IsMine) return;

        if (isMove)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            input = new Vector2(inputX, inputY);
            input *= speed;
            RB.velocity = input.normalized * speed;
            anim.SetBool("Walk", false);


            //anim.SetFloat("Walk", Mathf.Abs(inputX));

            //if (inputX > 0 && facingRight)
            //{
            //    FlipXRPC();
            //}
            //else if (inputX < 0 && !facingRight)
            //{
            //    FlipXRPC();
            //}

            if (inputX != 0)
            {
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, inputX);
                anim.SetBool("Walk", true);
                //walkAudio.Play();
            }
            //else
            //    walkAudio.Stop();

            NM.PointLight2D.transform.position = transform.position + new Vector3(0, 0, 10);
        }
        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    //void Move()
    //{
    //    if (isMove)
    //    {

    //    }
    //}


    [PunRPC]
    void FlipXRPC(float axis) => SR.flipX = axis == 1;





    //[PunRPC]
    //void FlipXRPC()
    //{
    //    //facingRight = !facingRight;
    //    //curScale = transform.localScale; // 자식이 뒤집히도록 만들어야한낟
    //    //curScale.x *= -1;
    //    //transform.localScale = curScale;
    //   }

    public void SetPos(Vector3 target)
    {
        transform.position = target;
    }

    [PunRPC] //이게 그냥 두개 자체에 색깔을 더하는 역할의 함수. 
    public void SetColor(int _colorIndex)
    {
        // playerStaffLight2D.color = UM.colors[_colorIndex];

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


    [PunRPC]
    public void Punch() // 펀치 함수. 
    {
        //플립한 그곳에서 생성시키도록 다시 수정
        PhotonNetwork.Instantiate("Punch", transform.position + new Vector3(SR.flipX ? 9f : -9f, 0f, -1f),
                Quaternion.Euler(0, 0, -180))
            .GetComponent<PhotonView>().RPC("DirRPC", RpcTarget.All, SR.flipX ? 1 : -1);

        StartCoroutine(UM.PunchCoolCo());
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

    IEnumerator WaitforCo()
    {
        yield return new WaitForSeconds(2f);
    }

    [PunRPC]
    void DestroyPlayer() => Destroy(gameObject);



    public override void OnEnable()
    {
        playerInputAction.UI.Enable();
        playerInputAction.UI.InventoryOnOff.performed += OnInventoryOnOff;
    }

    public override void OnDisable()
    {
        playerInputAction.UI.InventoryOnOff.performed -= OnInventoryOnOff;
        playerInputAction.UI.Disable();

    }

    private void OnInventoryOnOff(InputAction.CallbackContext _)
    {
        GameManager.instance.InvenUI.InventoryOnOffSwitch();
    }


    public Vector3 ItemDropPosition(Vector3 inputPos)
    {
        Vector3 result = Vector3.zero;
        Vector3 toInputPos = inputPos - transform.position;
        if (toInputPos.sqrMagnitude > dropRange * dropRange)
        {
            result = transform.position + toInputPos.normalized * dropRange;
        }
        else
        {
            result = inputPos;
        }

        return result;
    }

}

