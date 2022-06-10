using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiedItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    /// <summary>
    /// �ʵ忡 �������� �����Ҷ� setItem�� ���ؼ� 
    /// ���޹��� item���� ���� Ŭ������ item�� �ʱ�ȭ�Ѵ�. ��
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
