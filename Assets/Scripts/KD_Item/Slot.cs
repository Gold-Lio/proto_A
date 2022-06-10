using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Item item;
    public Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

   // public Item item;
   // public Image itemIcon; //슬롯안에 들어갈 이미지

   // //itemIcon.Sprite를 아이템 이미지로 초기화하고 활성화 시켜줌.
   // //주머니에 들어갈 아이템을 갱신
   //public void UpdateSlotUI()
   // {
   //     itemIcon.sprite = item.itemImage;
   //     itemIcon.gameObject.SetActive(true);
   // }

   // //사용하며 사라질 슬롯 안의 아이템
   // public void RemoveSlot()
   // {
   //     item = null;
   //     itemIcon.gameObject.SetActive(false);

}
