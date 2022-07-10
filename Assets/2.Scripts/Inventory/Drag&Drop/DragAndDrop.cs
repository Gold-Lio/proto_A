using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler  , IDragHandler          
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("포인터 다운");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 시작 ");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 끝");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("온 드래그");
    }
}
