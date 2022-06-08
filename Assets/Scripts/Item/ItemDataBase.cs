using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase DB;

    private void Awake() //DB에 자체 먹여준다.
    {
        DB = this;
    }

    public List<Item> itemDB = new List<Item>(); // Item에 직접 넣어주는 

    public GameObject fieldItemPrefab;
    public Vector2[] pos; //생성할 위치의 배열

    //필드위에 pos의 위치에 5개생성한뒤,  
    private void Start()
    {
        for (int i = 0; i < 6; i++) // 6개 생성
        {
           GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,3)]);
        }
    }
}
