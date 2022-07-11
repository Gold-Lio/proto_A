using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform       canvas; //최상단 Canvas.
    private Transform       previousParent; // 해당 obj가 직전에 속해있었던 obj의 위치
    private RectTransform   rect; //UI위치를 제어해줄 rect
    private CanvasGroup     canvasGroup; // Ui 알파 값과 상호작용할 캔버스 그룹 투명칠해준다.

    //캔버스를 여러개 사용하거나 아이템 오브젝트를 코드로 생성할경우
    //Awake대신 Setup 등의 퍼블릭 메서드를 통해서 매개변수 캔버스 변수 값을 설정 
    private void Start()
    {
        canvas      = FindObjectOfType<Canvas>().transform;
        rect        = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //드래그 직전 소속되어 있던 부모 Transform정보 저장
        previousParent = transform.parent;

        //현재 드래그 중인 UI가 화면의 최상단에 출력되기 위해
        transform.SetParent(canvas); // 부모 오브젝트를 Canvas로 설정
        transform.SetAsLastSibling(); // 가장 앞에 보이도록 마지막 자식으로 설정

        //드래그 가능한 오브젝트가 하나가 아닌 자식들을 가지고 잇을 수도 있기때문에 캔버스 그룹으로 통제
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //드래그 하는 동안 UI가 마우스를 따라다니도록. 
        //현재 스크린상의 마우스 위치를 UI위치로 설정.
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //드래그를 시작하면 부모가 canvas로 설정되어있기 때문에
        //드래그를 종료할때 부모가 canvas면 아이템 슬롯이 아닌 다른곳에 스폰됨. 
        //드롭을 했다는 뜻이기때문에, 드래그 직전에 소속되어 있던 아이템 슬롯으로 이동

        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
