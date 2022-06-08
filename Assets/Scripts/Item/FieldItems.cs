using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;


    //�ʵ忡 �����Ҷ� ���޹��� item���� ���� Ŭ������ item�� �ʱ�ȭ �Ѵ�. ����
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;

        image.sprite = item.itemImage;
    }

    //�������� ȹ�������� ����ϴ� GetItem

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()  // �ʵ��� �������� �ı��ϴ� �Լ�
    {
        Destroy(gameObject);
    }
}
