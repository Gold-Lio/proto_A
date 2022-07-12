using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;
    

    //�̹��� ���İ�����. 
    private void SetColor(float _alpha)
    {
        Color color = itemIcon.color;
        color.a = _alpha;
        itemIcon.color = color;
    }

    //itemIcon.sprite�� ������ �̹����� �ʱ�ȭ �ѵ�, Ȱ��ȭ �����ش�.
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
        SetColor(1);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
        SetColor(0);
    }

}
