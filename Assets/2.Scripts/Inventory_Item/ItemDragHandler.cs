using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviourPun, IDragHandler, IEndDragHandler
{
    public IInventoryItem Item { get; set; }

    public void OnDrag(PointerEventData eventData)
    {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            transform.localPosition = Vector2.zero;
    }
}
