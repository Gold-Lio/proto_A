using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; // ȹ���� ������
  //  public int itemCount; // ȹ���� �������� ����
    public Image itemImage;  // �������� �̹���

    // ������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }


    // �κ��丮�� ���ο� ������ ���� �߰�
    //�Ѿ���� ������
    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemImage;

        SetColor(1); // �������� ���Ա⶧���� ���İ� 1�� ����
    }

   
    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        //itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        //text_Count.text = "0";
        //go_CountImage.SetActive(false);
    }
}
