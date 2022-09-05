using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    // 변수 ---------------------------------------------------------------------------------------
    // 슬롯에 있는 아이템(ItemData)
    ItemData slotItemData;

    // 프로퍼티 ------------------------------------------------------------------------------------

    /// <summary>
    /// 슬롯에 있는 아이템(ItemData)
    /// </summary>
    public ItemData SlotItemData
    {
        get => slotItemData;
        private set
        {
            if (slotItemData != value)
            {
                slotItemData = value;
                onSlotItemChange?.Invoke();  // 변경이 일어나면 델리게이트 실행(주로 화면 갱신용)
            }
        }
    }

    // 델리게이트 ----------------------------------------------------------------------------------
    /// <summary>
    /// 슬롯에 들어있는 아이템의 종류나 갯수가 변경될 때 실행되는 델리게이트
    /// </summary>
    public System.Action onSlotItemChange;

    // 함수 ---------------------------------------------------------------------------------------

    /// <summary>
    /// 생성자들
    /// </summary>
    public ItemSlot() { }
    public ItemSlot(ItemData data, uint count)
    {
        slotItemData = data;
    }
    public ItemSlot(ItemSlot other)
    {
        slotItemData = other.SlotItemData;
    }

    /// <summary>
    /// 슬롯에 아이템을 설정하는 함수 
    /// </summary>
    /// <param name="itemData">슬롯에 설정할 ItemData</param>
    /// /// <param name="count">슬롯에 설정할 아이템 갯수</param>
    public void AssignSlotItem(ItemData itemData)
    {
        SlotItemData = itemData;
    }


    /// <summary>
    /// 슬롯을 비우는 함수
    /// </summary>
    public void ClearSlotItem()
    {
        SlotItemData = null;
    }


    // 함수(백엔드) --------------------------------------------------------------------------------

    /// <summary>
    /// 슬롯이 비었는지 확인해주는 함수
    /// </summary>
    /// <returns>true면 비어있는 함수</returns>
    public bool IsEmpty()
    {
        return slotItemData == null;
    }
}