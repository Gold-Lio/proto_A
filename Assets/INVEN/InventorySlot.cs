using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image itemIcon;
    public Text itemCount_Text;

    public GameObject selected_Item; 

    public void AddItem(Item item)
    {
        itemIcon.sprite = item.itemImage;
        if(Item.ItemType.Use == item.itemType)
        {
            if(item.itemCount > 0)
            {
                itemCount_Text.text = "x" + item.itemCount.ToString();
            }
            else
            {
                itemCount_Text.text = "";
            }
        }
    }
 
    public void RemoveItem()
    {
        itemCount_Text.text = "";
        itemIcon.sprite = null;
    }
}
