using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;

public class Inventory_UI : MonoBehaviour 
{
 //   public PhotonView PV;

    public GameObject inventoryPanel;
    bool activeInventory = false;

    private void Start()
    {
        inventoryPanel.SetActive(activeInventory);
    }

    private void Update()
    {
        //if (PV.IsMine)
        //{
            if (Input.GetKeyDown(KeyCode.I))
            {
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
            }
    }
}
