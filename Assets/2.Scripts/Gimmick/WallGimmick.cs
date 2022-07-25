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

    //��ư�� �̰��� ��������. 
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

}
