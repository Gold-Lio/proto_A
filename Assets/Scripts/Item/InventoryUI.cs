using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventoryPanel;

    bool isActiveInventory = false;

    private void Start()
    {//인벤토리 초기화
        inventoryPanel.SetActive(isActiveInventory);
    }
}
