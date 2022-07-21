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

    //��ư�� �̰��� ��������. 
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

    //������ ��Ÿ��
    //�����  ����ϱ���� Text ���� ���ҽ��ϴ�
    //IEnumerator DoorCoolCo(int doorIndex)
    //{
    //    //Debug.Log
    //    //yield return new WaitForSeconds(18);
       
    //    //DoorMaps[doorIndex].interactable = false;
    //    //yield return new WaitForSeconds(18);
    //    //DoorMaps[doorIndex].interactable = true;
    //}



}
