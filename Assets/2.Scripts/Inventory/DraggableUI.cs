using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform       canvas; //�ֻ�� Canvas.
    private Transform       previousParent; // �ش� obj�� ������ �����־��� obj�� ��ġ
    private RectTransform   rect; //UI��ġ�� �������� rect
    private CanvasGroup     canvasGroup; // Ui ���� ���� ��ȣ�ۿ��� ĵ���� �׷� ����ĥ���ش�.

    //ĵ������ ������ ����ϰų� ������ ������Ʈ�� �ڵ�� �����Ұ��
    //Awake��� Setup ���� �ۺ� �޼��带 ���ؼ� �Ű����� ĵ���� ���� ���� ���� 
    private void Start()
    {
        canvas      = FindObjectOfType<Canvas>().transform;
        rect        = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //�巡�� ���� �ҼӵǾ� �ִ� �θ� Transform���� ����
        previousParent = transform.parent;

        //���� �巡�� ���� UI�� ȭ���� �ֻ�ܿ� ��µǱ� ����
        transform.SetParent(canvas); // �θ� ������Ʈ�� Canvas�� ����
        transform.SetAsLastSibling(); // ���� �տ� ���̵��� ������ �ڽ����� ����

        //�巡�� ������ ������Ʈ�� �ϳ��� �ƴ� �ڽĵ��� ������ ���� ���� �ֱ⶧���� ĵ���� �׷����� ����
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�巡�� �ϴ� ���� UI�� ���콺�� ����ٴϵ���. 
        //���� ��ũ������ ���콺 ��ġ�� UI��ġ�� ����.
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //�巡�׸� �����ϸ� �θ� canvas�� �����Ǿ��ֱ� ������
        //�巡�׸� �����Ҷ� �θ� canvas�� ������ ������ �ƴ� �ٸ����� ������. 
        //����� �ߴٴ� ���̱⶧����, �巡�� ������ �ҼӵǾ� �ִ� ������ �������� �̵�

        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
