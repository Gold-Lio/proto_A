using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HealthBar : MonoBehaviourPun
{
    public GameObject myHealthBar;
    public Image hpImage;
    public Image hpEffectImage;

    public Animator dieAnim;    
    public bool playerCanDie = false;

    [HideInInspector] public float hp;
    [SerializeField] private float maxHp;
    [SerializeField] private float hurtSpeed;

    private void Start()
    {
        hp = maxHp;
        myHealthBar.SetActive(false);

        GameManager.instance.isPlayerDie += Die;
    }
   


    private void Update()
    {
        if(photonView.IsMine)
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

            Die(); //�� ���� ������ �����ϱ� ����Ƽ�� �ñ�. � �� ������ ������������ �����ϵ��� ���巯����. 
        }
    }


    private void Die()
    {
        if(photonView.IsMine)
        {
            if (hp <= 0)
            {
                    dieAnim.SetTrigger("Die");
                    GameManager.instance.playerDie();
                // �÷��̾ �����ؼ� �ִϸ��̼� ����. 
                // ���� �ڿ� �÷��̾� Destory.
                // �׸��� �� �÷��̾�� ��� �÷��̾�, ī�޶�� ��ȯ. 
            }
        }
    }
}
