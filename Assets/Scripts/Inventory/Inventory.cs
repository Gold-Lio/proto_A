using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private int slotCount = 5;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(instance);
    //    }
    //}

    public bool AddItem(Item _item)
    {
        if (items.Count < slotCount)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FieldItem"))
        {
            FieldItem fieldItem = col.GetComponent<FieldItem>();
            if (AddItem(fieldItem.GetItem()))
            {
                fieldItem.DestoryItem();
            }
        }
    }
}
