using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public event Action actPlayerDie; // 플레이어 사망시
    public event Action actGameStart; // 게임 처음 시작
    public event Action actGameEnd; // 게임 끝
}

public partial class GameManager : MonoBehaviour
{
    public void playerDie(UIManager obj)
    {
        if (obj is UIManager) // 이 이벤트는 UIManager에서만 호출 가능하다.
        {
            actPlayerDie?.Invoke(); // null 아니라면 호출
        }
    }

    public void GameStart(NetworkManager obj)
    {
        if (obj is NetworkManager) // 이 이벤트는 StartButton에서만 호출 가능하다.
        {
            actGameStart?.Invoke();
        }
    }

    public void GameEnd(NetworkManager obj)
    {
        if (obj is NetworkManager) // 이 이벤트는 StartButton에서만 호출 가능하다.
        {
            actGameEnd?.Invoke();
        }
    }

}
