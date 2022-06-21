using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
    void Start()
    {


    }


    // ������Ʈ�� �����Ӵ� �� �� ȣ��˴ϴ� 
    void Update()
    {


    }
    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.position = Input.mousePosition;  
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    transform.localPosition = Vector2.zero;
    //}
}
