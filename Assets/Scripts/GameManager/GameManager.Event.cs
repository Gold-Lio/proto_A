using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public event Action actPlayerDie; // �÷��̾� �����
    public event Action actGameStart; // ���� ó�� ����
    public event Action actGameEnd; // ���� ��
}

public partial class GameManager : MonoBehaviour
{
    public void playerDie(UIManager obj)
    {
        if (obj is UIManager) // �� �̺�Ʈ�� UIManager������ ȣ�� �����ϴ�.
        {
            actPlayerDie?.Invoke(); // null �ƴ϶�� ȣ��
        }
    }

    public void GameStart(NetworkManager obj)
    {
        if (obj is NetworkManager) // �� �̺�Ʈ�� StartButton������ ȣ�� �����ϴ�.
        {
            actGameStart?.Invoke();
        }
    }

    public void GameEnd(NetworkManager obj)
    {
        if (obj is NetworkManager) // �� �̺�Ʈ�� StartButton������ ȣ�� �����ϴ�.
        {
            actGameEnd?.Invoke();
        }
    }

}
