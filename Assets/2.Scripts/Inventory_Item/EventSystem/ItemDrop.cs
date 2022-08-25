using Photon.Voice.Unity.Demos.DemoVoiceUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public Inventory Inventory;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            IInventoryItem item = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().Item;
            if (item != null)
            {
                Inventory.RemoveItem(item);
                item.OnDrop();
                Debug.Log("Dorp");
            }
        }
    }
}
 