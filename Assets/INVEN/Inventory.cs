using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private InventorySlot[] slots;
    public Transform tf; //�θ�ü

    private List<Item> inventoryItemList;

    public GameObject go;
    private bool activated; //�κ��丮 Ȱ��ȭ�� true

    private void Start()
    {
        inventoryItemList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public void ShowTab()
    {
        RemoveSlot();
    }

    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
        }
    }


    private void Update()
    {//�����ν� �÷��̾ �ƹ��ൿ���� ���ϵ��� ���´�. KD _ OrderManager ã�ƺ���

        ShowTab();
    }

}
