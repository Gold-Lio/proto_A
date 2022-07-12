using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;


    //SetItem메서드를 통해 필드에 아이템 생성, 매개변수는 _Item을 가짐
    // 전달받은 Item으로 현재 클래스의 item을 초기화 함.
    // 
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemtype = _item.itemtype;

        image.sprite = _item.itemImage;
    }

    //아이템을 획득했을때 사용될
    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
