using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public event Action isPlayerDie; // �÷��̾� �����
    public event Action isPlayerDamage; // �÷��̾ ������ �Ծ�����
    public event Action isMummyDie; // �ֳʹ� �����

    public event Action GameStart; // ���� ó�� ����
    public event Action GameEnd; // ���� ��
    public event Action GamePause; // ���� �Ͻ� ����


}

public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public void playerDie(UIManager obj)  //�ش� �Ŵ������Լ��� ȣ��. 
    {
        if (obj is UIManager)
        {
            isPlayerDie?.Invoke();
        }
    }


    //�Ʒ����� �Լ� �� �������. 
}
