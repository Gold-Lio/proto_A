using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public GameObject inventoryPanel;
    public bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

    public void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();

        inventory.OnaddItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
    }

    //이 함수는 반복문을 통해서 슬롯을 초기화 하고, item의 갯수만큼 slot을 채워넣는다.
    private void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
