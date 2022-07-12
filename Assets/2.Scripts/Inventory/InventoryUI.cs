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

    //�� �Լ��� �ݺ����� ���ؼ� ������ �ʱ�ȭ �ϰ�, item�� ������ŭ slot�� ä���ִ´�.
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
