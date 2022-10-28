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

    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip hitClip; // �ǰ� �Ҹ�
    public AudioClip itemPickupClip; // ������ ���� �Ҹ�

    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    PhotonView PV;

    PlayerScript MyPlayer;

    public bool isDie;
    
    private void Awake()
    {
        PV = photonView;
        // ����� ������Ʈ�� ��������
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
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();

    }

    // ü�� ȸ��
    [PunRPC]
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestoreHealth() ���� (ü�� ����)
        base.RestoreHealth(newHealth);
        // ü�� ����
        hpImage.fillAmount = health;
    }

    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();


        playerCanDie = true;
        // ü�� �����̴� ��Ȱ��ȭ
        myHealthBar.gameObject.SetActive(false);
        // ����� ���
        playerAudioPlayer.PlayOneShot(deathClip);
        // �ִϸ������� Die Ʈ���Ÿ� �ߵ����� ��� �ִϸ��̼� ���
        playerAnimator.SetTrigger("Die");

        //�ƿ��������̰Բ� ó��
      
    }


    private void Update()
    {
        HeathDisappearAnimation();

    }

    [PunRPC]
    public void Hit()
    {
        hp -= attackDamage;  //1���ϸ� �ѹ��̰� 0.5�� �ϸ� 50��/...? �̰� ����ü ���� ���ΰ�
        //hpImage.fillAmount -= 0.5f;
        if (hpImage.fillAmount <= 0)
        {
            PV.RPC("DestroyPlayer", RpcTarget.AllBuffered); // AllBuffered�� �ؾ� ����� ����� �������װ� �� �����
           
            PhotonNetwork.Instantiate("PlayerDeadStone", transform.position, Quaternion.identity);

            isDie = true;

            //WinCheck���� �� �����°ɱ�?? ������ ������ �ִ°��ϱ�?
            //������� �������� ������ ����. �׷��� ������� �ʰ� �Ȱ��� 1 1 �����ؼ� ���� Set�� ���� ����. 
            NM.WinCheck();
        }
    }

    public void HeathDisappearAnimation()
    {
        //ü�� ��� ���
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
            //Die(); //�� ���� ������ �����ϱ� ����Ƽ�� �ñ�.
            //� �� ������ ������������ �����ϵ��� ���巯����. 
            //���ÿ����÷ο� �߻�. 
        }
    }
    
    public void AfterDie()
    {
        if(isDie)
        {
            //�г� �����ϰ� , ������ UI�� ������, ī�޶� ����ġ. 
        }
    }
}

