using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    
    void Pickup()
    {
        InventoryManager.instance.Add(item);
        Destroy(gameObject);
        Debug.Log("������ ȹ��");
    }


    public void OnMouseDown()
    {
        Pickup();
    }

}
