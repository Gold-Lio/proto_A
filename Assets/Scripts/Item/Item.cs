using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    ERelics,  //유물
    ETreasure //보물
}


[System.Serializable] // 어트리뷰트 
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use() //아이템 성공여부를 반환해야함. 
    {
        return false; 
    }
}




//}
//public class Item : MonoBehaviour
//{
//    public string itemName;
//    public Sprite itemImage;

//    public GameObject itemPrefab;

//    public enum ItemType
//    {
//        Erelics,  //유물
//        Etreasure //보물
//    }

//    private void Start()
//    {
//        //  ItemType = 
//    }



