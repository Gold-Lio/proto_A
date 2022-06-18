using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Sword";
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

    //게임 개체를 비활성화한다. 
    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}
