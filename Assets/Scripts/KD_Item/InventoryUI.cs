using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    

    public GameObject inventroyPanel;
    public bool pickupActivated = false;
    [SerializeField]
    private Text broadCastText;

    private void Start()
    {//�κ��丮�г� �ʱ�ȭ
        inventroyPanel.SetActive(false);
    }
    private void Update()
    {
        if(pickupActivated)
        { 

        }
    }


    public void PickUp()
    {

    }



    [PunRPC]
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Item") && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            pickupActivated = true; // �ݱ� ��ư  �̺�Ʈ ������ Ȱ��ȭ  
            ItemInfoAppear();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Item") && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            pickupActivated = false;  // �ݱ� ��ư  �̺�Ʈ ������ ��Ȱ��ȭ
            InfoDisappear();
        }
    }



    //�������� �����ðڽ��ϱ� TEXT ����
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        broadCastText.gameObject.SetActive(true);
    }

    //�������� �����ðڽ��ϱ� TEXT ������� �Լ�
    private void InfoDisappear()
    {
        pickupActivated = false;
        broadCastText.gameObject.SetActive(false);
    }


}
