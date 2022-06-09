using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;
using UnityEngine.UI;


public class ActionController : MonoBehaviourPun
{
    [SerializeField]
    private bool pickupActivated = false;

    [SerializeField]
    private Text broadCastText;

    //������ ���̾���� �������. 
    private LayerMask layerMask;


    private void Update()
    {
        TryAction();
    }

    //��� �� ActionController.cs�� player���Լ� �پ��־�� �Ѵ�. 
    //��Ҵٸ� ��ư�� Ȱ��ȭ ��Ű�� 
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



    private void TryAction()
    {
        CanPickUp();
    }


    private void CanPickUp()
    {
        if (pickupActivated)
        {
          //  Destroy(("Item");

            //�������� �� �ֺ��� �־ 
            //�� ��ư�� ��������!
            // �������� �������! ���� 
            //

            
            //�����ֿ�� �ְ� ����������
        }
        else
        {
            return;
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

//private void Update()
//{
//    CheckItem();
//    TryAction();
//}

//��ư Ȱ��ȭ


//��ư�� �����ٸ� UI������ �Ȱ��� ���� ����. 

//�׸���  �ش� �κ��丮 ������ �������
// �����°����� �����Ͽ� ó��
// ���� localplayer�ֺ����� ��ü.addforce�� ������������ ���� 

