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

//�κ��丮 �׸��� �ѷ��� ������ ����, �̺�Ʈ�� �߻��Ҷ� �Ű������� ���Ǵ� �κ��丮 �̺�Ʈ �μ� Ŭ������ �ִ� ����. 
public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}
