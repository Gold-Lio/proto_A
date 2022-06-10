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

    #region �̱���
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
            items.Add(_item);// �������߰� ������
            if(onChangeItem != null)
            onChangeItem.Invoke();//onChangeItemȣ��. 
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

//    //items��� �̸��� Item�� ����Ʈ ����ϸ�
//    //items�� ������ slotCnt (���� Ȱ�� ����)���� �������� 
//    //Ȱ��ȭ�ǵ���. 

//    //������ �߰��� �����ҽ� true�� �߰� ���н� false�� ���. 
//    public bool AddItem(Item _item)
//    {
//        if (items.Count < slotCount)
//        {
//            items.Add(_item);

//            if (onChangeItem != null)
//            {
//                onChangeItem.Invoke();    //���� ��������Ʈ�� �� ��������? ������ �߰��� �����ϸ�, onChangeItem�� �߰�. 
//                Debug.Log("additem true");
//                return true;
//            }
//        }
//        Debug.Log("additem false");
//        return false;
//    }

//    //�÷��̾ fielditem�� ������ä� 
//    //�ʵ� �������� �ı����ش�. 
//    public void OnTriggerEnter2D(Collider2D col)
//    {
//        if (col.CompareTag("Item"))
//        {
//            Debug.Log("�����ۿ� ��Ҵ�"); //���� ü�� ������ �ִ� ��Ȳ. 
//            FiedItems fiedItems = col.GetComponent<FiedItems>();

//            //AddItem�� �������� �߰��Ǹ� True�� ��ȯ�Ѵ�. 
//            //�׷��� Destory�� �Ǵ°�.

//            //�ٵ� �����ۿ� �߰����� �ʾұ� ������ destory�� ���� �ʴ°Ŵ�.. 
//            if (AddItem(fiedItems.GetItem()))
//            {
//                fiedItems.DestoryItem();
//            }
//        }
//    }
//    /// <summary>
//    /// �븮�� ����, �븮�� �ν��Ͻ� ȭ
//    /// set�ȿ��� �븮�� ȣ���Ѵ�. 
//    /// </summary>
//}
