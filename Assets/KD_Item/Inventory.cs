using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    //�⺻ �κ��丮 ���̽�
    [SerializeField]
    private GameObject Go_InventoryBase;

    private GameObject go_SlotParnet;

    private Slot[] slots;


    //��ŸƮ ����,,,������ ���������� ��ü ���̶�. 
    private void Start()
    {
        slots = go_SlotParnet.GetComponentsInChildren<Slot>();
    }


    //�������� ä���ִ� �Լ�
    public void AcquireItem(Item _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            return;
        }
  
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.itemName == "")
            {
                slots[i].AddItem(_item);
                return;
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
}
