using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // 기본 데이터 ---------------------------------------------------------------------------------
    /// <summary>
    /// 아이템 슬롯 아이디
    /// </summary>
    protected uint id;

    /// <summary>
    /// 이 슬롯UI에서 가지고 있을 ItemSlot(inventory클래스가 가지고 있는 ItemSlot중 하나)
    /// </summary>
    protected ItemSlot itemSlot;

    // 주요 인벤토리 UI 가지고 있기 -----------------------------------------------------------------

    /// <summary>
    /// 인벤토리 UI
    /// </summary>
    InventoryUI invenUI;

    /// <summary>
    /// 상세 정보창
    /// </summary>
    protected DetailInfoUI detailUI;

    // UI처리용 데이터 -----------------------------------------------------------------------------

    /// <summary>
    /// 아이템의 Icon을 표시할 이미지 컴포넌트
    /// </summary>
    protected Image itemImage;

    
    // 프로퍼티들 ----------------------------------------------------------------------------------

    /// <summary>
    /// 아이템 슬롯 아이디(읽기 전용)
    /// </summary>
    public uint ID { get => id; }

    /// <summary>
    /// 이 슬롯UI에서 가지고 있을 ItemSlot(읽기 전용)
    /// </summary>
    public ItemSlot ItemSlot { get => itemSlot; }

    // 함수들 --------------------------------------------------------------------------------------
    protected virtual void Awake()  // 오버라이드 가능하도록 virtual 추가
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();    // 아이템 표시용 이미지 컴포넌트 찾아놓기
    }

    /// <summary>
    /// ItemSlotUI의 초기화 작업
    /// </summary>
    /// <param name="newID">이 슬롯의 ID</param>
    /// <param name="targetSlot">이 슬롯이랑 연결된 ItemSlot</param>
    public virtual void Initialize(uint newID, ItemSlot targetSlot)
    {
        invenUI = GameManager.instance.InvenUI; // 미리 찾아놓기
        detailUI = invenUI.Detail;

        id = newID;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChange = Refresh; // ItemSlot에 아이템이 변경될 경우 실행될 델리게이트에 함수 등록        
    }

    /// <summary>
    /// 슬롯에서 표시되는 아이콘 이미지 갱신용 함수
    /// </summary>
    public void Refresh()
    {
        if (itemSlot.SlotItemData != null)
        {
            // 이 슬롯에 아이템이 들어있을 때
            itemImage.sprite = itemSlot.SlotItemData.itemIcon;  // 아이콘 이미지 설정하고
            itemImage.color = Color.white;  // 불투명하게 만들기
        }
        else
        {
            // 이 슬롯에 아이템이 없을 때
            itemImage.sprite = null;        // 아이콘 이미지 제거하고
            itemImage.color = Color.clear;  // 투명하게 만들기
        }
    }

    /// <summary>
    /// 슬롯위에 마우스 포인터가 들어왔을 때
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemSlot.SlotItemData != null)
        {
            //Debug.Log($"마우스가 {gameObject.name}안으로 들어왔다.");
            detailUI.Open(itemSlot.SlotItemData);
        }
    }

    /// <summary>
    /// 슬롯위에서 마우스 포인터가 나갔을 때
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log($"마우스가 {gameObject.name}에서 나갔다.");
        detailUI.Close();
    }

    /// <summary>
    /// 슬롯위에서 마우스 포인터가 움직일 때
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerMove(PointerEventData eventData)
    {
        //Debug.Log($"마우스가 {gameObject.name}안에서 움직인다.");
        Vector2 mousePos = eventData.position;

        // 상세정보창이 화면을 벗어났는지 체크
        RectTransform rect = (RectTransform)detailUI.transform;
        if ((mousePos.x + rect.sizeDelta.x) > Screen.width)
        {
            mousePos.x -= rect.sizeDelta.x; // 벗어났으면 상세정보창을 마우스 왼쪽으로 이동시킴)
        }

        detailUI.transform.position = mousePos; // 상세정보창을 마우스 커서 위치로 변경
    }

    /// <summary>
    /// 슬롯을 마우스로 클릭했을 때
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // 마우스 왼쪽 버튼 클릭일 때
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TempItemSlotUI temp = invenUI.TempSlotUI;
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
        }
    }
}
