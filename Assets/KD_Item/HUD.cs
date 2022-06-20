using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Inventory inventory;

    //���۰� ���ÿ� 
    private void Start()
    {
        inventory.ItemAdded += Inventory_Script_ItemAdded;
    }

    private void Inventory_Script_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");

        Debug.Log("ã�Ҵ�");
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                break;
            }
        }
    }
}
