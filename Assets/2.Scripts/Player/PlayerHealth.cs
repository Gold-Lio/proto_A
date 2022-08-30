using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;
using static UIManager;


public class PlayerHealth : LivingEntity
{
    [SerializeField]
    private float attackDamage;

    public GameObject myHealthBar;
    public Image hpImage;
    public Image hpEffectImage;

    public Animator dieAnim;
    public bool playerCanDie = false;

    [HideInInspector] public float hp;
    [SerializeField] private float maxHp;
    [SerializeField] private float hurtSpeed;

    public AudioClip deathClip; // 사망 소리
    public AudioClip hitClip; // 피격 소리
    public AudioClip itemPickupClip; // 아이템 습득 소리

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    private Animator playerAnimator; // 플레이어의 애니메이터

    PhotonView PV;

    PlayerScript MyPlayer;

    public bool isDie;
    
    private void Awake()
    {
        PV = photonView;
        // 사용할 컴포넌트를 가져오기
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();
    }

    private void Start()
    {
        hp = maxHp;
        myHealthBar.SetActive(false);
    }

    protected override void OnEnable()
    {
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

    }

    // 체력 회복
    [PunRPC]
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        base.RestoreHealth(newHealth);
        // 체력 갱신
        hpImage.fillAmount = health;
    }

    public override void Die()
    {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();


        playerCanDie = true;
        // 체력 슬라이더 비활성화
        myHealthBar.gameObject.SetActive(false);
        // 사망음 재생
        playerAudioPlayer.PlayOneShot(deathClip);
        // 애니메이터의 Die 트리거를 발동시켜 사망 애니메이션 재생
        playerAnimator.SetTrigger("Die");

        //아예못움직이게끔 처리
      
    }


    private void Update()
    {
        HeathDisappearAnimation();

    }

    [PunRPC]
    public void Hit()
    {
        hp -= attackDamage;  //1로하면 한방이고 0.5로 하면 50방/...? 이게 도대체 무슨 일인가
        //hpImage.fillAmount -= 0.5f;
        if (hpImage.fillAmount <= 0)
        {
            PV.RPC("DestroyPlayer", RpcTarget.AllBuffered); // AllBuffered로 해야 제대로 사라져 복제버그가 안 생긴다
           
            PhotonNetwork.Instantiate("PlayerDeadStone", transform.position, Quaternion.identity);

            isDie = true;

            //WinCheck에서 왜 막히는걸까?? 빠지는 이유가 있는것일까?
            //사라지는 시점에서 먹히지 않음. 그래서 사라지지 않고 똑같이 1 1 검출해서 게임 Set이 되지 않음. 
            NM.WinCheck();
        }
    }

    public void HeathDisappearAnimation()
    {
        //체력 닳는 모션
        if (photonView.IsMine)
        {
            myHealthBar.SetActive(true);

            hpImage.fillAmount = hp / maxHp;

            if (hpEffectImage.fillAmount > hpImage.fillAmount)
            {
                hpEffectImage.fillAmount -= hurtSpeed;
            }

            else
            {
                hpEffectImage.fillAmount = hpImage.fillAmount;
            }
            //Die(); //그 많은 검출을 때리니까 유니티가 팅김.
            //어떤 한 조건을 만족햇을때만 실행하도록 만드러야함. 
            //스택오버플로우 발생. 
        }
    }
    
    public void AfterDie()
    {
        if(isDie)
        {
            //패널 생성하고 , 나머지 UI다 제거후, 카메라 스위치. 
        }
    }
}

