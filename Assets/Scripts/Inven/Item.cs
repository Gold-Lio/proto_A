using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public int itemID;               // 아이템의 고유 ID값 , 중복불가능
    public string itemName; // 아이템의 이름 , 중복가능
    public string itemDescription; //아이템 설명? 
    public int itemCount;    //아이템 갯수
    public Sprite itemIcon; // 아이템의 아이콘
    public ItemType itemType;
                                        
    public enum ItemType
    {
        Item,
        Weapon,
        Use,
    }

    //생성자를 통해서 채워주는 역할을 하는 Item(int )
    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount =1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("RPG_inventory_icons/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }


    private void Start()
    {
}
}
