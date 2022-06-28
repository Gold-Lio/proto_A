using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem : MonoBehaviour
{
    [SerializeField]
    private float range;  // 아이템 습득이 가능한 최대 거리
    private Inventory theInventory;
    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    private RaycastHit hitInfo;  // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

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
