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
    public event Action isMummyDie; // 애너미 사망시

    public event Action GameStart; // 게임 처음 시작
    public event Action GameEnd; // 게임 끝
    public event Action GamePause; // 게임 일시 정지


}

public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public void playerDie(UIManager obj)  //해당 매니저에게서만 호출. 
    {
        if (obj is UIManager)
        {
            isPlayerDie?.Invoke();
        }
    }


    //아래에서 함수 더 적어야함. 
}
