using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem : MonoBehaviour
{
    [SerializeField]
    private float range;  // ������ ������ ������ �ִ� �Ÿ�
    private Inventory theInventory;
    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 
    private RaycastHit hitInfo;  // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    private void Update()
    {
        ChekItem();
    }

    private void ChekItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            Debug.DrawLine(hitInfo.transform.position,Vector3.forward * 100, Color.red);
            if (hitInfo.transform.tag == "Item")
            {
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }

}
