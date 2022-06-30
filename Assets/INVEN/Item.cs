using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item// 게임 오브젝트에 붙일 필요 X 
{
    public string itemName; // 아이템의 이름
    public int itemCount;
    public Sprite itemImage; // 아이템의 이미지(인벤 토리 안에서 띄울)
    public ItemType itemType; // 아이템 유형

    public enum ItemType  // 아이템 유형
    {
        Item,
        Use,
        ETC,
    }


    //item을 생성자로 사용
    public Item(string itemName, int itemCount, ItemType itemType)
    {
        this.itemName = itemName;
        this.itemCount = itemCount;
        this.itemType = itemType;

        itemImage = Resources.Load("Resources/Item" + itemName.ToString(), typeof(Sprite)) as Sprite;
    }
}
