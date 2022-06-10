using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiedItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    /// <summary>
    /// 필드에 아이템을 생성할때 setItem을 통해서 
    /// 전달받은 item으로 현재 클래스의 item을 초기화한다. ㅗ
    /// </summary>
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
    
        image.sprite = _item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }
        

    public void DestoryItem()
    {
        Destroy(this.gameObject); ;
    }
}
