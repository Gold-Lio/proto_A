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

    public delegate void OnSlotCountChange(int val); // 대리자 정의
    public OnSlotCountChange onSlotCountChange; // 대리자 인스턴스화

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

    //items의 갯수가 SlotCnt 현재 활성 슬롯 보다 작을때만 추가되도록
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
            //AddItem에 아이템이 추가되면 True를 반환하니까,  이것을 사용해서 아이템추가에 성공한다면 해당 필드 아이템은 파괴한다.
             FieldItems fieldItems = col.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
                fieldItems.DestroyItem();
        }
    }
}
