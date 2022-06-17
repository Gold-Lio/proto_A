using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {


    private Inventory inventory;
    public int index;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
         new WaitForSeconds(3f);
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        if (transform.childCount <= 0) {
            inventory.items[index] = 0;
        }
    }

    public void Cross() {

        foreach (Transform child in transform) {
            child.GetComponent<Spawn>().SpawnItem();
            GameObject.Destroy(child.gameObject);
        }
    }

}
