using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase DB;

    private void Awake() //DB�� ��ü �Կ��ش�.
    {
        DB = this;
    }

    public List<Item> itemDB = new List<Item>(); // Item�� ���� �־��ִ� 

    public GameObject fieldItemPrefab;
    public Vector2[] pos; //������ ��ġ�� �迭

    //�ʵ����� pos�� ��ġ�� 5�������ѵ�,  
    private void Start()
    {
        for (int i = 0; i < 6; i++) // 6�� ����
        {
           GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,3)]);
        }
    }
}
