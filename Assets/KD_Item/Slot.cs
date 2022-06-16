using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemImage;


    //�̹��� ���� ����
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }


    //������ �߰�
    public void AddItem(Item _item)
    {
        item = _item;

        itemImage.sprite = item.itemImage;

         SetColor(1);
    }

    //�ɷ����� ������. 
    //������ ������ �����ų �� �ִ�  ����ϴ� �Լ�?
    public void SetSlotCount(int _count)
    {
        
    }

    //���� ��ü �ʱ�ȭ
    private void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }
}
