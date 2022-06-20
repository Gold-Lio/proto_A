using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;

    public GameObject MessagePanel;

    // Use this for initialization
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        int index = -1;
        foreach (Transform slot in inventoryPanel)
        {
            index++;

            //    // Border... Image
            //    Transform imageTransform = slot.GetChild(0).GetChild(0).GetComponent<Inventory>();
            //    //ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            //    if (index == e.Item.Slot.Id)
            //    {
            //        image.enabled = true;
            //        image.sprite = e.Item.Image;

            //        int itemCount = e.Item.Slot.Count;
            //        if (itemCount > 1)
            //            txtCount.text = itemCount.ToString();
            //        else
            //            txtCount.text = "";


            //        // Store a reference to the item
            //        itemDragHandler.Item = e.Item;

            //        break;
            //    }
            //}
        }

    }
}

