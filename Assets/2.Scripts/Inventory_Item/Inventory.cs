using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Inventory : MonoBehaviourPun
{
    private const int SLOTS = 5;
    private const int Max = 6;

    private List<IInventoryItem> mItems = new List<IInventoryItem>(); //인터페이스의 List 변수 mitems를 만듦

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public void AddItem(IInventoryItem item)
    {  //주울 아이템슬롯의 갯수 < 내가 가지고 있는 슬롯갯수{
        if (mItems.Count < SLOTS)
        {//사용가능한 무료 슬롯이 있는 경우에만 발생하는 Collider 논리
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>(); //g항목의 픽업 방법을 이벤트로써
            if (collider.enabled)
            {
                collider.enabled = false;

                mItems.Add(item);
                Debug.Log($"현재 카운트는 : {mItems.Count} 입니다.");
                item.OnPickup(); //IInvnetory의 인터페이스의 OnPickup실행

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }


    //실행은 되지만 빠져나감으로써ㅡ HUD의 Inventory_ItemRemoved가 아예작동하지 않음.
    public void RemovedItem(IInventoryItem item)
    {

        if (mItems.Contains(item))
        {
            Debug.Log($"현재 카운트는 : {mItems.Count} 입니다.");

            mItems.Remove(item);
            item.OnDrop();

            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();

            if (collider != null)
            {
                collider.enabled = true;
            }

            Debug.Log($"현재 카운트는 : {mItems.Count} 입니다.");
            //ItemRemove자체가 뜨지 않는다. 이벤튿가 없다 왜?? 왜 없는거야?

            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}

