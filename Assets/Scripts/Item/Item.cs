using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    ERelics,  //����
    ETreasure //����
}


[System.Serializable] // ��Ʈ����Ʈ 
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use() //������ �������θ� ��ȯ�ؾ���. 
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
//        Erelics,  //����
//        Etreasure //����
//    }

//    private void Start()
//    {
//        //  ItemType = 
//    }



