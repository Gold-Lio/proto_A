using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static PlayerScript;

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

    PhotonView PV;

    private void Start()
    {
        PV = photonView;

        hp = maxHp;
        myHealthBar.SetActive(false);
    }

    private void Update()
    {
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

            if (hp <= 0)
            {
                PS.PlayerDie();
            }
        }
    }
}
