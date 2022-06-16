using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //기본 인벤토리 베이스
    [SerializeField]
    private GameObject Go_InventoryBase;

    private GameObject go_SlotParnet;

    private Slot[] slots;


    //게임이 시작될때 ui, network 에서 시작할때 넣어줄 함수
    public void SetSlots()
    {
        slots = go_SlotParnet.GetComponentsInChildren<Slot>();
    }


    //아이템을 채워넣는 함수
    public void AcquireItem(Item _item)
    {
        //if (Item.ItemType.Treasure != _item.itemType)
        //{
        //    for (int i = 0; i < slots.Length; i++)
        //    {
        //        if (slots[i].item != null)
        //        {
        //            if (slots[i].item.itemName == _item.itemName)
        //            {//들어가 있다면, 갯수를 증가시킨다 인데...나는 꽉찼을 경우만 구현하면 된다. 
        //                slots[i].SetSlotCount(_count);
        //                return;
        //            }
        //        }
        //    }

        for (int i = 0; i < slots.Length; i++)
        {
                if (slots[i].item.itemName == null)
                {
                    slots[i].AddItem(_item);
                    return;
                }
            }
        }
    }

//private void TryOpenInventory()
//{
//    if(Input.GetKeyDown(KeyCode.I))
//    { 
//        if(inven)
//    }
//}
