using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    private const int SLOT = 2;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;


    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < SLOT)
        {
            Collider2D col = (item as MonoBehaviour).GetComponent<Collider2D>(); 
            if(col.enabled)
            {
                col.enabled = false;
                mItems.Add(item);

                item.OnPickUp();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);

            item.OnDrop();

            Collider2D col = (item as MonoBehaviour).GetComponent<Collider2D>();
            
            if(col != null)
            {
                col.enabled = true;
            }

            if(ItemRemoved !=null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}


