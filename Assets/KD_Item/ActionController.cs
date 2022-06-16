using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using static UIManager;
using static NetworkManager;

public class ActionController : MonoBehaviourPun
{
    public static ActionController instance;
    public bool pickupActivated = false;


    [SerializeField]
    public Inventory theInventory;
    private Collider2D col;


    ////필요한 컴포넌트 
    //[SerializeField]
    //private Text actionText;

    public void FindInventory()
    {
        GameObject.Find("Inventory");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            pickupActivated = true;
            UM.SetInteractionBtn2(6, true);
        }
    }


    //현재 내가 닿은 게임오브젝트를 파괴하고 
    //canPickup에서 인벤토리.cs에 있는 정보를 가져와서 인벤토리에 sprite사진을 보여준다. 

    public void CanPickUp(Collider2D col)
    {
        if (pickupActivated)
        {
            Destroy(col.gameObject);
            theInventory.AcquireItem(col.transform.GetComponent<ItemPickUp>().item); // 부딛힌것의 itemPickup의 아이템을 가져온다. 
        }
    }
}
