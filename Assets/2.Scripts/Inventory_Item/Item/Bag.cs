using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Bag";
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

    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    //public void OnDrop()
    //{
    //    RaycastHit2D hit = new RaycastHit2D();
    //    Ray ray = Camera.main.WorldToScreenPoint(Input.mousePosition);
    //}

    public void OnDrop()
    {
        ////���߿��� �̰��� �÷��̾� ��ġ���� ���� �� �ֵ��� ����. 
        //Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 1000);

        //Debug.Log("Drop");

        //if (hit)
        //{
        //    gameObject.SetActive(true);
        //    gameObject.transform.position = hit.point;
        //}

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }


   
}
