using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> Items = new List<Item>();

    
    public Transform ItemContent;
    public GameObject inventoryItem;


    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    //���߿� �巡�� ����ϴ� �������� �������Ѵ�.
    // �� ������ ��ư���� �����ϴ� ���. 
    public void Remove(Item item)
    {
        Items.Remove(item);
    }



    //
    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(inventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemIcon.sprite = item.icon;
        }

    }



}
