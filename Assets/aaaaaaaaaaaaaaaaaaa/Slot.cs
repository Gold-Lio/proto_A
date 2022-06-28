using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; // 획득한 아이템
  //  public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }


    // 인벤토리에 새로운 아이템 슬롯 추가
    //넘어오는 아이템
    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemImage;

        SetColor(1); // 아이템이 들어왔기때문에 알파값 1로 변경
    }

   
    // 해당 슬롯 하나 삭제
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
