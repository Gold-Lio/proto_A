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

            Die(); //그 많은 검출을 때리니까 유니티가 팅김. 어떤 한 조건을 만족햇을때만 실행하도록 만드러야함. 
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
                // 플레이어에 접근해서 애니메이션 실행. 
                // 몇초 뒤에 플레이어 Destory.
                // 그리고 그 플레이어는 모든 플레이어, 카메라로 전환. 
            }
        }
    }
}
