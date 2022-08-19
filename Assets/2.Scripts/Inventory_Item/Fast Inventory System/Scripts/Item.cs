using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public Sprite Image;
    public int amount;

    public enum type { NormalItem, Weapon, Armor, Potion, Bag }
    public type selectedType;

    /*int count; //lifePotion?
      int attack;
      int defense;
      *
      
      //public Transform prefab;

    */

    public Item()
    {
        id = -1;
        selectedType = type.NormalItem;
    }
}
