using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IInventoryItem
{
     string Name { get; }
     Sprite Image { get; }

    void Onpickup();
}


public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;
}