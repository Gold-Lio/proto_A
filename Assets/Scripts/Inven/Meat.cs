using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Meat ";
        }
    }
    public Sprite _Image = null;
    
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void Onpickup()
    {
        gameObject.SetActive(true);
    }
}
