using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerHealth : LivingEntity
{
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

    private void Awake()
    {
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


    // 데미지 처리
    [PunRPC]
    public override void OnDamage(float damage) 
    {
        if (!dead)
        {
            // 사망하지 않은 경우에만 효과음을 재생
            playerAudioPlayer.PlayOneShot(hitClip);
        }

        // LivingEntity의 OnDamage() 실행(데미지 적용)
        base.OnDamage(damage);
        // 갱신된 체력을 체력 슬라이더에 반영
        hpImage.fillAmount = health;
    }



    public override void Die()
    {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();

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


    IEnumerator PlayerDie()
    {
        yield return null;
    }

}
