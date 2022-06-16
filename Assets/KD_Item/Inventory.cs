using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //�⺻ �κ��丮 ���̽�
    [SerializeField]
    private GameObject Go_InventoryBase;

    private GameObject go_SlotParnet;

    private Slot[] slots;


    //������ ���۵ɶ� ui, network ���� �����Ҷ� �־��� �Լ�
    public void SetSlots()
    {
        slots = go_SlotParnet.GetComponentsInChildren<Slot>();
    }


    //�������� ä���ִ� �Լ�
    public void AcquireItem(Item _item)
    {
        //if (Item.ItemType.Treasure != _item.itemType)
        //{
        //    for (int i = 0; i < slots.Length; i++)
        //    {
        //        if (slots[i].item != null)
        //        {
        //            if (slots[i].item.itemName == _item.itemName)
        //            {//�� �ִٸ�, ������ ������Ų�� �ε�...���� ��á�� ��츸 �����ϸ� �ȴ�. 
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
