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
    //            Debug.Log("현재 아이템이 다 찼습니다. 아이템을 버려주세요");
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
    //        //Player와 접촉하면 Getcompoenent로 inventory를 가져온다. 
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
    //            //    Debug.Log("현재 아이템이 다 찼습니다. 아이템을 버려주세요");
    //            //}
    //        }
    //    }
