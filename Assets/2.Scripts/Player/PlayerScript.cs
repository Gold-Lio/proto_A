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
using UnityEditor;

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



    // 인벤토리 및 아이템 관련 변수들 ----------------------------------------------------------------------------
    // 플레이어 인풋 액션 추가
    private PlayerInputAction playerInputAction;

    // 아이템 줍는 범위
    private float itemPickupRange = 5.0f;

    // 아이템 드랍 거리
    private float dropRange = 5.0f;

    // 인벤토리 클래스
    private Inventory inven;


    private void Awake()
    {
        PS = this;
        anim = gameObject.GetComponent<Animator>();

        // 플레이어 인풋 액션 추가 (드랍, 인벤토리 onoff)
        playerInputAction = new PlayerInputAction();
        //walkAudio = GetComponent<AudioSource>();

        // 인벤 클래스 생성
        inven = new Inventory();
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

        // 인벤토리UI 초기화
        GameManager.instance.InvenUI.InitializeInventory(inven);
        inven.AddItem(ItemIDCode.Test_Item);

        GameManager.instance.MainPlayer = this;
    }

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

            if (inputX != 0)
            {
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, inputX);
                anim.SetBool("Walk", true);
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


    //서로 겹치지 않도록 하는 Oncol.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), col.gameObject.GetComponent<CapsuleCollider2D>());
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
    
    
    
    
    
    // 인벤토리 및 아이템 관렴 함수들 -------------------------------------------------------------------------------

    public override void OnEnable()
    {
        playerInputAction.UI.Enable();
        playerInputAction.UI.InventoryOnOff.performed += OnInventoryOnOff;
        playerInputAction.UI.ItemPickUp.performed += OnItemPickUp;
    }

    public override void OnDisable()
    {
        playerInputAction.UI.ItemPickUp.performed -= OnItemPickUp;
        playerInputAction.UI.InventoryOnOff.performed -= OnInventoryOnOff;
        playerInputAction.UI.Disable();

    }

    private void OnInventoryOnOff(InputAction.CallbackContext _)
    {
        GameManager.instance.InvenUI.InventoryOnOffSwitch();
    }

    private void OnItemPickUp(InputAction.CallbackContext _)
    {
        //Collider[] cols = Physics.OverlapSphere(transform.position, itemPickupRange, LayerMask.GetMask("Item"));
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position, itemPickupRange, LayerMask.GetMask("Item"));
        foreach (var col in cols)
        {
            Item item = col.GetComponent<Item>();

            if (inven.AddItem(item.data))
            {
                GameManager.instance.InvenUI.Detail.IsPause = false;
                Destroy(col.gameObject);
            }
        }
    }

    public Vector3 OnItemDropPosition(Vector3 inputPos)
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

