using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image         image;
    private RectTransform rect;

    private void Start()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.yellow;

    }

    public void OnDrop(PointerEventData eventData)
    {
        //포인터 드래그는 현재 드래그 하고 있는 대상(아이템)
        if(eventData.pointerDrag != null)
        {
            //드래그 하고 있는 대상의 부모를 현재 오브젝트로 생성하고, 위치르 현재 오브젝트 위치와 동일하게 설정
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}
