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
        inventory.ItemAdded += Inventory_Script_ItemAdded;
        inventory.ItemRemoved += Inventory_Script_ItemRemoved;
    }

    //private void Inventory_Script_ItemAdded(object sender, InventoryEventArgs e)
    //{
    //    Transform inventoryPanel = transform.Find("InventoryPanel");

    //    Debug.Log("찾았다");
    //    foreach (Transform slot in inventoryPanel)
    //    {
    //        Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

    //        if (!image.enabled)
    //        {
    //            image.enabled = true;
    //            image.sprite = e.Item.Image;

    //            break;
    //        }
    //    }
    //}

    private void Inventory_Script_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            // Border... Image
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

          if(!image.enabled)
                {
                image.enabled = true;
                image.sprite = e.Item.Image;

                itemDragHandler.Item = e.Item;

                break;
            }
        }
    }

    private void Inventory_Script_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            // Border... Image
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();


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
