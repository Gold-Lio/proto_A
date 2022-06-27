using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Sword,
    Item,
    Rock
}
   


[System.Serializable]
public class Item
{
    public ItemType itemtype;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false;
    }

}
