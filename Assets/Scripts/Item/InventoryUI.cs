using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventoryPanel;

    bool isActiveInventory = false;

    private void Start()
    {//�κ��丮 �ʱ�ȭ
        inventoryPanel.SetActive(isActiveInventory);
    }
}
