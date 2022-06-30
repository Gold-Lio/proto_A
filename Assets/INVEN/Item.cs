using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item// ���� ������Ʈ�� ���� �ʿ� X 
{
    public string itemName; // �������� �̸�
    public int itemCount;
    public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���)
    public ItemType itemType; // ������ ����

    public enum ItemType  // ������ ����
    {
        Item,
        Use,
        ETC,
    }


    //item�� �����ڷ� ���
    public Item(string itemName, int itemCount, ItemType itemType)
    {
        this.itemName = itemName;
        this.itemCount = itemCount;
        this.itemType = itemType;

        itemImage = Resources.Load("Resources/Item" + itemName.ToString(), typeof(Sprite)) as Sprite;
    }
}
