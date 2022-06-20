using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static PickUp instance;
    public GameObject slotItem;
    public Inventory inventory;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            Inventory inventory = col.GetComponent<Inventory>();

            for (int i = 0; i < inventory.slots.Count; i++)
            {
                if (inventory.slots[i].isEmpty)
                {
                    Instantiate(slotItem, inventory.slots[i].slotObject.transform, false);
                    inventory.slots[i].isEmpty = false;
                    Destroy(this.gameObject);
                    break;
                }
            }
        }

    }

}
    //public void SetPickUp()
    //{
    //    StartCoroutine(PickUpCo());

    //    for (int i = 0; i < inventory.slots.Count; i++)
    //    {
    //        if (inventory.slots[i].isEmpty)
    //        {
    //            Instantiate(slotItem, inventory.slots[i].slotObject.transform, false);
    //            inventory.slots[i].isEmpty = false;
    //            Destroy(this.gameObject);
    //            break;
    //        }
    //        else if (!inventory.slots[i].isEmpty)
    //        {
    //            Debug.Log("���� �������� �� á���ϴ�. �������� �����ּ���");
    //        }
    //    }
    //}

    //IEnumerator PickUpCo()
    //{ Inventory inventory = GetComponent<Inventory>();
    //    yield return null;

    //}

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //   if (col.gameObject.CompareTag("Player"))
    //    {
    //        //Player�� �����ϸ� Getcompoenent�� inventory�� �����´�. 
    //        Inventory inventory = col.GetComponent<Inventory>();

    //        for (int i = 0; i < inventory.slots.Count; i++)
    //        {
    //            if (inventory.slots[i].isEmpty)
    //            {
    //                Instantiate(slotItem, inventory.slots[i].slotObject.transform, false);
    //                inventory.slots[i].isEmpty = false;
    //                Destroy(this.gameObject);
    //                break;
    //            }
    //            //else if (!inventory.slots[i].isEmpty)
    //            //{
    //            //    Debug.Log("���� �������� �� á���ϴ�. �������� �����ּ���");
    //            //}
    //        }
    //    }
