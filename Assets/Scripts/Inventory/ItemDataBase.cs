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
        //������ �ʵ�������� ��������  itemDB�� �Ѱ��� �ʱ�ȭ���ش�.
        for (int i = 0; i < 5; i++)
        {
           GameObject go = Instantiate(fieldItemPrefab, pos[i],Quaternion.identity);
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }

    public List<Item> itemDB = new List<Item>();





}
