using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public Inventory Inventory;
    private void Start()
    {
        Inventory.ItemAdded += inventoryScript_ItemAdded;
    }

    private void inventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            //Border Image.
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            //�� ������ ã�Ҵٸ�
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                //todo store a refernece to the item
                break;
            }
        }
    }
}
