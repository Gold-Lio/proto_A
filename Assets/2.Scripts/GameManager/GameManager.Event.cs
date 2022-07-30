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

    public event Action actGameStart; // ���� ó�� ����
    public event Action actGameEnd; // ���� ��

}

public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public void playerDie()  //�ش� �Ŵ������Լ��� ȣ��. 
    {
        isPlayerDie?.Invoke();
    }

    public void PlayerDamage()
    {
        isPlayerDamage?.Invoke();
    }

    public void GameStart()
    {
        actGameStart?.Invoke();
    }

    public void GameEnd()
    {
        actGameEnd?.Invoke();
    }

    //�Ʒ����� �Լ� �� �������. 
}
