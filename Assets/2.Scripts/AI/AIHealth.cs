using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AIHealth : MonoBehaviourPun
{
    public GameObject myHealthBar;
    public Image hpImage;
    public Image hpEffectImage;

    [HideInInspector] public float hp;
    [SerializeField] private float maxHp;
    [SerializeField] private float hurtSpeed;

    private void Start()
    {
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
        }
    }
}
