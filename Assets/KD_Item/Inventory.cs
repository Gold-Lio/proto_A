using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    //기본 인벤토리 베이스
    [SerializeField]
    private GameObject Go_InventoryBase;

    private GameObject go_SlotParnet;

    private Slot[] slots;


    //스타트 말고,,,게임이 시작했을때 전체 요이땅. 
    private void Start()
    {
        slots = go_SlotParnet.GetComponentsInChildren<Slot>();
    }


    //아이템을 채워넣는 함수
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
