using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("포인터 클릭");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 시작");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("엔드 드래그");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("온 드래그--");
    }
}
