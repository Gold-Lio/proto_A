using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;


    //필드에 선언할때 전달받은 item으로 현재 클래스에 item을 초기화 한다. ㅎㅎ
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;

        image.sprite = item.itemImage;
    }

    //아이템을 획득했을때 사용하는 GetItem

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()  // 필드의 아이템을 파괴하는 함수
    {
        Destroy(gameObject);
    }
}
