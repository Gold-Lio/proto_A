using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public event Action isPlayerDie; // 플레이어 사망시
    public event Action isPlayerDamage; // 플레이어가 데미지 입었을때

    public event Action actGameStart; // 게임 처음 시작
    public event Action actGameEnd; // 게임 끝

}

public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public void playerDie()  //해당 매니저에게서만 호출. 
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

    //아래에서 함수 더 적어야함. 
}
