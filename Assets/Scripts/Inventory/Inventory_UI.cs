using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;

public class Inventory_UI : MonoBehaviour 
{
    Inventory inven;
    Slot[] slots;
    public Transform slotHolder;

    // 스타트하자마자? 시작되는 문제. 
    public void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onChangeItem += RedrawSlotUI;
    }
    public void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
