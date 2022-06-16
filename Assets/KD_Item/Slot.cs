using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemImage;


    //이미지 투명도 조절
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }


    //아이템 추가
    public void AddItem(Item _item)
    {
        item = _item;

        itemImage.sprite = item.itemImage;

         SetColor(1);
    }

    //걸러내도 괜찮음. 
    //슬롯의 개수를 변경시킬 수 있는  사용하는 함수?
    public void SetSlotCount(int _count)
    {
        
    }

    //슬롯 전체 초기화
    private void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }
}
