using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;


public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;

    public GameObject fieldItemPrefab;
    public Vector3[] pos;
    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        //생성된 필드아이템의 아이템을  itemDB중 한개로 초기화해준다.
        for (int i = 0; i < 5; i++)
        {
           GameObject go = Instantiate(fieldItemPrefab, pos[i],Quaternion.identity);
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }

    public List<Item> itemDB = new List<Item>();





}
