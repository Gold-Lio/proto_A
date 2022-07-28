using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInput : MonoBehaviourPun
{
    PhotonView PV;
    private float h;
    private float v;

 
    public void Update()
    {
        if(GameManager.instance != null && GameManager.instance.isDead)
        {
            //나머지 수치 0
        }
    }


}
