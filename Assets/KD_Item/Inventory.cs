using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NetworkManager;
public class Inventory : MonoBehaviour {

    public static Inventory instance;
    public int[] items;
    public GameObject[] slots;

    public void Start()
    {
        slots[0] = GameObject.Find("Slot(0)");
        slots[1] = GameObject.Find("Slot(1)");
        slots[2] = GameObject.Find("Slot(2)");
        slots[3] = GameObject.Find("Slot(3)");
        slots[4] = GameObject.Find("Slot(4)");
    }
  
}
