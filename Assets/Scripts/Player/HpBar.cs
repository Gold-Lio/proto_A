using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class HpBar : MonoBehaviourPunCallbacks
{
    public static HpBar instance;
    public Slider hpBar;
    public float maxHp;
    public float curHP;
    private void Update()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, curHP / maxHp, Time.deltaTime * 5f); 
    }
}
