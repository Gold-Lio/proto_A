﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 5;

    private List<IInventoryItem> mItems = new List<IInventoryItem>(); //인터페이스의 List 변수 mitems를 만듦

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < SLOTS) //주울 아이템슬롯의 갯수 < 내가 가지고 있는 슬롯갯수
        {//사용가능한 무료 슬롯이 있는 경우에만 발생하는 Collider 논리
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>(); //g항목의 픽업 방법을 이벤트로써
            if(collider.enabled)
            {
                mItems.Add(item);

                item.OnPickup(); //IInvnetory의 인터페이스의 OnPickup실행

                //추가된 항목이 발생후, 이벤트의 모든 구독자에게 알림이 간다. 

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
}