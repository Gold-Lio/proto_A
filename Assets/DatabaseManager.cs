using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 1.  ���̵� �� �̺�Ʈ
/// 2.  ���̺� , �ε�
/// 3. ������ ������
/// </summary>


public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instace;
    private void Awake()
    {
        if (instace != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instace = this;
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;


    public List<Item> itemList = new List<Item>();


    private void Start()
    {
        //itemList.Add(new Item("apple", 1, itemType.Item));
        //itemList.Add(new Item("armor", 1, ItemType.Item));
        //itemList.Add(new Item("axe", 1, ItemType.Item));
        //itemList.Add(new Item("bag", 1, ItemType.Item));
        //itemList.Add(new Item("book", 1, ItemType.Item));
    }
}
