using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;
using UnityEngine.UI;

public class WallGimmick : MonoBehaviourPun
{
    public static WallGimmick instance;
    PhotonView PV;
    public GameObject[] wall;
    public GameObject wallEyeOpen;
    public GameObject wallEyeClose;


    private void Start()
    {
        PV = photonView;
        wallEyeOpen.SetActive(true);
        wallEyeClose.SetActive(false); //�ʹ� �ʱ�ȭ
    }

    [PunRPC]
    public void DoorMapClickRPC()
    {
        wall[0].SetActive(true);
        wall[1].SetActive(true);
        wallEyeOpen.SetActive(false);
        wallEyeClose.SetActive(true);
        StartCoroutine(DoorCoolCo()); //���� 2�ʸ� ����.
    }

    IEnumerator DoorCoolCo()
    {
        yield return new WaitForSeconds(5.0f); //5�� �ڿ� ���� �����. 
        wall[0].SetActive(false);
        wall[1].SetActive(false);
        wallEyeOpen.SetActive(true);
        wallEyeClose.SetActive(false);
    }
}
