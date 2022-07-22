using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;

public class WallGimmick : MonoBehaviourPun
{
    PhotonView PV;


    private void Start()
    {
        PV = photonView;
    }

    //버튼이 이것을 눌러야함. 
    public void DoorMapClick(int doorIndex)
    {
        PV.RPC("DoorMapClickRPC", RpcTarget.AllViaServer, doorIndex);
    }

    [PunRPC]
    void DoorMapClickRPC(int doorIndex)
    {
        StartCoroutine(DoorCo(doorIndex));
      //  StartCoroutine(DoorCoolCo(doorIndex));
    }

    IEnumerator DoorCo(int doorIndex)
    {
        NM.wall[doorIndex].SetActive(true);
        yield return new WaitForSeconds(7);
        NM.wall[doorIndex].SetActive(false);
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
