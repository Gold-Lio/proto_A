using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 5;

    private List<IInventoryItem> mItems = new List<IInventoryItem>(); //인터페이스의 List 변수 mitems를 만듦

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < SLOTS) //주울 아이템슬롯의 갯수 < 내가 가지고 있는 슬롯갯수
        {//사용가능한 무료 슬롯이 있는 경우에만 발생하는 Collider 논리
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>(); //g항목의 픽업 방법을 이벤트로써
            if (collider.enabled)
            {
                collider.enabled = false;

                mItems.Add(item);

                item.OnPickup(); //IInvnetory의 인터페이스의 OnPickup실행

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    public void RemovedItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnDrop();

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}
//    public static Inventory instance;
//    //슬롯 데이타 리스트를 만들고 슬롯을 관리해준다.
//    public List<SlotData> slots = new List<SlotData>();
//    private int maxSlot = 5;

//    public GameObject slotPrefab;

//    public void Start()
//    {
//        //slot을 프리팹화 한 후 panel아래에 maxSlot만큼 생성
//        GameObject inventoryPanel = GameObject.Find("InventoryPanel");

//        for (int i = 0; i < maxSlot; i++)
//        {
//            GameObject go = Instantiate(slotPrefab, inventoryPanel.transform, false);
//            //리스트에 isempty와 슬롯오브젝트르르 담아서 추가해준다.
//            go.name = "Slot_" + i;
//            SlotData slot = new SlotData();
//            slot.isEmpty = true;
//            slot.slotObject = go;
//            slots.Add(slot);

//        }
//    }
//}


