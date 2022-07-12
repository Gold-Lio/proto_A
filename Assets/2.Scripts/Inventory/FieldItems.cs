using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;


    //SetItem�޼��带 ���� �ʵ忡 ������ ����, �Ű������� _Item�� ����
    // ���޹��� Item���� ���� Ŭ������ item�� �ʱ�ȭ ��.
    // 
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemtype = _item.itemtype;

        image.sprite = _item.itemImage;
    }

    //�������� ȹ�������� ����
    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
