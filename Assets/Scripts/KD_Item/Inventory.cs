using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    //public delegate void OnSlotCountChange(int val);
    //public OnSlotCountChange onSlotCountChange;


    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    //public delegate void OnChangeItem();
    //public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    public int slotCount;

    #region 싱글턴
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }
    }
    #endregion

    public int SlotCount
    {
        get => slotCount;
        set
        {
            slotCount = value;
            //   onSlotCountChange.Invoke(slotCount);
        }
    }

    private void Start()
    {
        SlotCount = 5;
    }

    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)
        {
            items.Add(_item);// 아이템추가 성공시
            if(onChangeItem != null)
            onChangeItem.Invoke();//onChangeItem호출. 
            return true;
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            FiedItems fiedItems = col.GetComponent<FiedItems>();
            if (AddItem(fiedItems.GetItem()))
            {
                fiedItems.DestoryItem();
            }
        }
    }





}

//    //items라는 이름의 Item의 리스트 사용하며
//    //items의 개수가 slotCnt (현재 활성 슬롯)보다 작을때만 
//    //활성화되도록. 

//    //아이템 추가에 성공할시 true가 뜨고 실패시 false가 뜬다. 
//    public bool AddItem(Item _item)
//    {
//        if (items.Count < slotCount)
//        {
//            items.Add(_item);

//            if (onChangeItem != null)
//            {
//                onChangeItem.Invoke();    //여기 델리게이트가 왜 들어가야하지? 아이템 추가에 성공하면, onChangeItem을 추가. 
//                Debug.Log("additem true");
//                return true;
//            }
//        }
//        Debug.Log("additem false");
//        return false;
//    }

//    //플레이어가 fielditem과 닿았을시ㅡ 
//    //필드 아이템을 파괴해준다. 
//    public void OnTriggerEnter2D(Collider2D col)
//    {
//        if (col.CompareTag("Item"))
//        {
//            Debug.Log("아이템에 닿았다"); //현재 체가 씹히고 있는 상황. 
//            FiedItems fiedItems = col.GetComponent<FiedItems>();

//            //AddItem에 아이템이 추가되면 True를 반환한다. 
//            //그래서 Destory가 되는것.

//            //근데 아이템에 추가되지 않았기 때문에 destory가 되지 않는거다.. 
//            if (AddItem(fiedItems.GetItem()))
//            {
//                fiedItems.DestoryItem();
//            }
//        }
//    }
//    /// <summary>
//    /// 대리자 정의, 대리자 인스턴스 화
//    /// set안에서 대리자 호출한다. 
//    /// </summary>
//}
