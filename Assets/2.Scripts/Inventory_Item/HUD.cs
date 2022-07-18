using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Inventory inventory;

    //시작과 동시에 
    private void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }
    
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            // Border... Image
            Transform imageTransform = slot.GetChild(0).GetChild(0); 
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            //we found the empty slot
           if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                //store a reference to the item
                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            // Border... Image  
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
            

            if (itemDragHandler.Item == null)
                continue;

            if (itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;
                break;
            }
        }
    }
}
