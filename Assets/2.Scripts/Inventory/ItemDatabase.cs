using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class ItemDatabase : MonoBehaviourPun
{
    public static ItemDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector3[] pos;


    //반복문으로 6개를 만들어주고 생성된 FieldItem의 Item을 itemDB중 한개로 초기화해준다.
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
          GameObject go = Instantiate(fieldItemPrefab, pos[i],Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,3)]);
        }
    }

}   
