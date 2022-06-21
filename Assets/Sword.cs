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

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }


    //일단 그냥 3d라도 해보자. 

    public void OnDrop()
    {
        //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ray2D ray = new Ray2D(pos, Vector2.zero);
        //Physics2D.Raycast(ray.origin, ray.direction);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //if (Physics2D.Raycast(ray.origin, ray.direction))
        //{
        //    gameObject.SetActive(true);
        //    gameObject.transform.position = Input.mousePosition;    // hit.point;
        //}

        //Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ray2D ray = new Ray2D(wp, Vector2.zero);
        //RaycastHit2D hit = new RaycastHit2D();


        //if (Physics2D.Raycast(ray.origin, ray.direction, 1))
        //{
        //    gameObject.SetActive(true);
        //    gameObject.transform.position = hit.point;
        //}


        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 터치한 좌표 가져옴
        Ray2D ray = new Ray2D(wp, Vector2.zero); // 원점에서 터치한 좌표 방향으로 Ray를 쏨

        float distance = Mathf.Infinity; // Ray 내에서 감지할 최대 거리

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance);

        if (hit)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }

        //    Physics2D.Raycast(ray, Vector2.zero, 1000);

        //if(hit)
        //{
        //    gameobject.setactive(true);
        //    gameobject.transform.position = hit.point;
        //}

        //Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = new RaycastHit2D();

        //if (Physics2D.Raycast(ray, Vector2.zero, 1000))
        //{
        //    gameObject.SetActive(true);
        //  //    gameObject.transform.position = hit.point;
        //  //}

        //  Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //  RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 1000000);

        //  if(hit)
        //  {
        //      gameObject.SetActive(true);
        //      gameObject.transform.position = hit.point;
        //  }

        ////  if (Physics2D.Raycast(ray, out hit,1000))
    }
}
