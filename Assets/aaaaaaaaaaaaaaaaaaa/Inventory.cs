using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 사실상 쓸일 없음. 
    // 인벤토리 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.
    public static bool invectoryActivated = false;

    [SerializeField] 
    private GameObject go_InventoryUI;  // Slot들의 부모인 Grid Setting 
    private Slot[] slots;  // 슬롯들 배열

    void Start()
    {
        slots = go_InventoryUI.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem( Item _item) //*, int _count = 1*/)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item); ///*, _count*/);
                return;
            }
        }
    }
}
