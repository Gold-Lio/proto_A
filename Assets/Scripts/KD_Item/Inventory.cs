using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;


    public delegate void OnChnageItem();
    public OnChnageItem onChnageItem;

    public List<Item> items = new List<Item>();

    public int slotCount;
    public int SlotCnt
    {
        get => slotCount;
        set
        {
            slotCount = value;
            onSlotCountChange.Invoke(slotCount);
        }
    }

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

    private void Start()
    {
        slotCount = 5;
    }

    //items��� �̸��� Item�� ����Ʈ ����ϸ�
    //items�� ������ slotCnt (���� Ȱ�� ����)���� �������� 
    //Ȱ��ȭ�ǵ���. 

    //������ �߰��� �����ҽ� true�� �߰� ���н� false�� ���. 
    public bool AddItem(Item _item)
    {
        if (items.Count < slotCount)
        {
            items.Add(_item);
            if (onChnageItem != null)
            {
                onChnageItem.Invoke();
                return true;
                Debug.Log("additem true");
            }
        }

        Debug.Log("additem false");
        return false;
    }


    //�÷��̾ fielditem�� ������ä� 
    //�ʵ� �������� �ı����ش�. 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            Debug.Log("�����ۿ� ��Ҵ�"); //���� �̰� ��ü�� ������ �ִ� ��Ȳ. 
            FiedItems fiedItems = col.GetComponent<FiedItems>();
            if (AddItem(fiedItems.GetItem()))
            {
                fiedItems.DestoryItem();
            }
        }
    }


    /// <summary>
    /// �븮�� ����, �븮�� �ν��Ͻ� ȭ
    /// set�ȿ��� �븮�� ȣ���Ѵ�. 
    /// </summary>


}
