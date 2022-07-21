using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;

public class WallGimmick : MonoBehaviourPun
{
    PhotonView PV;
    public GameObject[] wall;

    private void Start()
    {
        PV = photonView;
    }

    //버튼이 이것을 눌러야함. 
    public void DoorMapClick()
    {
        PV.RPC("DoorMapClickRPC", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void DoorMapClickRPC()
    {
        StartCoroutine(DoorCo());
    }

    IEnumerator DoorCo()
    {

        yield return null; 
    }

    //벽막기 쿨타임
    //디버그  사용하기까지 Text 몇초 남았습니다
    //IEnumerator DoorCoolCo(int doorIndex)
    //{
    //    //Debug.Log
    //    //yield return new WaitForSeconds(18);
       
    //    //DoorMaps[doorIndex].interactable = false;
    //    //yield return new WaitForSeconds(18);
    //    //DoorMaps[doorIndex].interactable = true;
    //}



}
