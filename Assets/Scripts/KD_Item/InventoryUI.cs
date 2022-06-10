using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public GameObject inventroyPanel;
    public bool pickupActivated = false;
    [SerializeField]
    //  private Text broadCastText;

 //   [Space(20)]
    public Slot[] slots;
    public Transform slotHolder;

    //��ŸƮ���� inventory�� �ʱ�ȭ
    //onslotCOuntChnage�� ������ �޼��带 �����Ѵ�. += �����ڻ��. 
    
    //onChnageItem�� ������ �޼��带 �����Ѵ�. RedrawSlotUI;
    private void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        // inventory.onSlotCountChange += SlotChange;
        inventory.onChangeItem += RedrawSlotUI;
        //�κ��丮�г� �ʱ�ȭ
        inventroyPanel.SetActive(false);
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    ////�ݺ����� ���ؼ� ���Ե��� �ʱ�ȭ�ϰ� item�� ������ŭ slot�� ä���ִ´�.
    //void RedrawSlotUI()
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        slots[i].RemoveSlot();
    //    }
    //    for (int i = 0; i < inventory.items.Count; i++)
    //    {
    //        slots[i].item = inventory.items[i];
    //        slots[i].UpdateSlotUI();
    //    }
    //}












}
    //private void SlotChange(int val)
    //{
    //    for (int i = 0; i < 6; i++)
    //    {
    //        if (i < inventory.SlotCnt)
    //        {
    //            slots[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //}




    //private void Update()
    //{
    //    if (pickupActivated)
    //    {

    //    }
    //}


    //public void PickUp()
    //{

    //}



//    [PunRPC]
//    private void OnCollisionEnter2D(Collision2D col)
//    {
//        if (col.gameObject.CompareTag("Item") && col.gameObject.GetComponent<PhotonView>().IsMine)
//        {
//            pickupActivated = true; // �ݱ� ��ư  �̺�Ʈ ������ Ȱ��ȭ  
//            ItemInfoAppear();
//        }
//    }

//    private void OnCollisionExit2D(Collision2D col)
//    {
//        if (col.gameObject.CompareTag("Item") && col.gameObject.GetComponent<PhotonView>().IsMine)
//        {
//            pickupActivated = false;  // �ݱ� ��ư  �̺�Ʈ ������ ��Ȱ��ȭ
//            InfoDisappear();
//        }
//    }

//    //�������� �����ðڽ��ϱ� TEXT ����
//    private void ItemInfoAppear()
//    {
//        pickupActivated = true;
//       // broadCastText.gameObject.SetActive(true);
//    }

//    //�������� �����ðڽ��ϱ� TEXT ������� �Լ�
//    private void InfoDisappear()
//    {
//        pickupActivated = false;
// //       broadCastText.gameObject.SetActive(false);
//    }


//}

