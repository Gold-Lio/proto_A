using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;
using UnityEngine.UI;


public class ActionController : MonoBehaviourPun
{
    [SerializeField]
    private bool pickupActivated = false;

    [SerializeField]
    private Text broadCastText;

    //아이템 레이어에서만 반응토록. 
    private LayerMask layerMask;


    private void Update()
    {
        TryAction();
    }

    //고로 이 ActionController.cs는 player에게서 붙어있어야 한다. 
    //닿았다면 버튼을 활성화 시키고 
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



    private void TryAction()
    {
        CanPickUp();
    }


    private void CanPickUp()
    {
        if (pickupActivated)
        {
          //  Destroy(("Item");

            //아이템이 내 주변에 있어서 
            //그 버튼을 눌렀더니!
            // 아이템은 사라진다! 까지 
            //

            
            //직접주울수 있게 만들어줘야해
        }
        else
        {
            return;
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

//private void Update()
//{
//    CheckItem();
//    TryAction();
//}

//버튼 활성화


//버튼을 눌렀다면 UI에서도 똑같은 유뮬 생성. 

//그리고  해당 인벤토리 영역을 벗어낫을때
// 버리는것으로 간주하여 처리
// 현재 localplayer주변에서 강체.addforce로 랜덤방향으로 생성 

