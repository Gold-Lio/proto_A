using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Item,
    Treasure
}


[System.Serializable]
public class Item 
{
    public EItemType itemtype;
    public string itemName;
    public Sprite itemImage;


    //»ç¿ë
    public bool Use()
    {
        return false;
    }
}
