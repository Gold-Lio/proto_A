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


    ////�ʿ��� ������Ʈ 
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


    //���� ���� ���� ���ӿ�����Ʈ�� �ı��ϰ� 
    //canPickup���� �κ��丮.cs�� �ִ� ������ �����ͼ� �κ��丮�� sprite������ �����ش�. 

    public void CanPickUp(Collider2D col)
    {
        if (pickupActivated)
        {
            Destroy(col.gameObject);
            theInventory.AcquireItem(col.transform.GetComponent<ItemPickUp>().item); // �ε������� itemPickup�� �������� �����´�. 
        }
    }
}
