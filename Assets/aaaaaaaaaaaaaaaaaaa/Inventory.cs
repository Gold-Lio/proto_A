using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // ��ǻ� ���� ����. 
    // �κ��丮 Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.
    public static bool invectoryActivated = false;

    [SerializeField] 
    private GameObject go_InventoryUI;  // Slot���� �θ��� Grid Setting 
    private Slot[] slots;  // ���Ե� �迭

    void Start()
    {
        slots = go_InventoryUI.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem( Item _item) //*, int _count = 1*/)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item); ///*, _count*/);
                return;
            }
        }
    }
}
