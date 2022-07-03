using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    private const int SLOTS = 5;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < SLOTS)
        {
            BoxCollider2D col = (item as MonoBehaviour).GetComponent<BoxCollider2D>();
            if (col.enabled)
            {
                col.enabled = false;
                
                mItems.Add(item);
                
                item.Onpickup();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
}