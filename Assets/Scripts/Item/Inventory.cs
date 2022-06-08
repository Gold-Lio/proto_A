using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public const int SLOT_CUT = 5;

    public List<Item> items = new List<Item>();


    private void Start()
    {
        
    }

    //매직넘버인 
    public bool AddItem(Item _item)
    {
        if(items.Count < SLOT_CUT)
        {
            items.Add(_item);
        }
        return false;
    }



    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Relics"))
        {
            FieldItems fielditems = col.GetComponent<FieldItems>();
            if(AddItem(fielditems.GetItem()))
            {
                fielditems.DestroyItem();
            }

        }
    }

}
