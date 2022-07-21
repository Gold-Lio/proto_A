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
        wallEyeClose.SetActive(false); //초반 초기화
    }

    [PunRPC]
    public void DoorMapClickRPC()
    {
        wall[0].SetActive(true);
        wall[1].SetActive(true);
        wallEyeOpen.SetActive(false);
        wallEyeClose.SetActive(true);
        StartCoroutine(DoorCoolCo()); //벽을 2초만 생성.
    }

    IEnumerator DoorCoolCo()
    {
        yield return new WaitForSeconds(5.0f); //5초 뒤에 벽이 사라짐. 
        wall[0].SetActive(false);
        wall[1].SetActive(false);
        wallEyeOpen.SetActive(true);
        wallEyeClose.SetActive(false);
    }
}
