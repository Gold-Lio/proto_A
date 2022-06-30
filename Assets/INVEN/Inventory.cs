using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private InventorySlot[] slots;
    public Transform tf; //부모객체

    private List<Item> inventoryItemList;

    public GameObject go;
    private bool activated; //인벤토리 활성화시 true

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
    {//오더로써 플레이어가 아무행동하지 못하도록 막는다. KD _ OrderManager 찾아보기

        ShowTab();
    }

}
