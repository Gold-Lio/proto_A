using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    

    public GameObject inventroyPanel;
    public bool pickupActivated = false;
    [SerializeField]
    private Text broadCastText;

    private void Start()
    {//인벤토리패널 초기화
        inventroyPanel.SetActive(false);
    }
    private void Update()
    {
        if(pickupActivated)
        { 

        }
    }


    public void PickUp()
    {

    }



    [PunRPC]
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Item") && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            pickupActivated = true; // 줍기 버튼  이벤트 리스너 활성화  
            ItemInfoAppear();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Item") && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            pickupActivated = false;  // 줍기 버튼  이벤트 리스너 비활성화
            InfoDisappear();
        }
    }



    //아이템을 주으시겠습니까 TEXT 생성
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        broadCastText.gameObject.SetActive(true);
    }

    //아이템을 주으시겠습니까 TEXT 사라지는 함수
    private void InfoDisappear()
    {
        pickupActivated = false;
        broadCastText.gameObject.SetActive(false);
    }


}
