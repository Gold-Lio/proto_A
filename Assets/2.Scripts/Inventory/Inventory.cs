using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSlotCountChange(int val); // �븮�� ����
    public OnSlotCountChange onSlotCountChange; // �븮�� �ν��Ͻ�ȭ

    public delegate void OnChangeItem();
    public OnChangeItem OnaddItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
        }
    }

    private void Start()
    {
        SlotCnt = 2;
    }

    //items�� ������ SlotCnt ���� Ȱ�� ���� ���� �������� �߰��ǵ���
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (OnaddItem != null)
            OnaddItem.Invoke();
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Item"))
        {
            //AddItem�� �������� �߰��Ǹ� True�� ��ȯ�ϴϱ�,  �̰��� ����ؼ� �������߰��� �����Ѵٸ� �ش� �ʵ� �������� �ı��Ѵ�.
             FieldItems fieldItems = col.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
                fieldItems.DestroyItem();
        }
    }
}
