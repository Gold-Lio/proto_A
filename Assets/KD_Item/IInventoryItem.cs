using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInventoryItem
{ 
    string Name { get; }
    Sprite Image { get; }

    void OnPickup();

    void OnDrop();
}

//인벤토리 항목을 둘러싼 일종의 래퍼, 이벤트가 발생할때 매개변수로 사용되는 인벤토리 이벤트 인수 클래스가 있는 이유. 
public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}
