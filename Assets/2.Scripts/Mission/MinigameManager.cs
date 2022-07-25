using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;
using static NetworkManager;

public class MinigameManager : MonoBehaviour, IMinigame
{
    public int curRemainMission;
    int remainMission;

    public bool isMissioning = false;

    private void Start()
    {
    }

    public void StartMission()
    {
        isMissioning = true;
        remainMission = curRemainMission;
        gameObject.SetActive(true);
    }

    public void CancelMission()
    {
        isMissioning = false;
        gameObject.SetActive(false);
    }

    public void CompleteMission()
    {
        if (--remainMission <= 0)
        {
            UM.MissionClear(gameObject);
            //현재 있는 게임 오브젝트 사라짐. 
            NM.Interactions[UM.curInteractionNum].SetActive(false);
        }
    }

}
