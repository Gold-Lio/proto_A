using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
public class ItemDropHandler : MonoBehaviourPun, IDropHandler
{
    public Inventory inventory;

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            Debug.Log("onDrop");
            IInventoryItem item = eventData.pointerDrag.gameObject.GetComponent<ItemDragHandler>().Item;
            if (item != null)
            {
                inventory.RemovedItem(item);
                item.OnDrop();
            }
        }

        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
