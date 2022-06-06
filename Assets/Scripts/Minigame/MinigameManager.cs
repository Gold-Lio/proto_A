using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;
using static NetworkManager;

public class MinigameManager : MonoBehaviour, IMinigame
{
    public int curRemainMission;
	int remainMission;

	public void StartMission()
    {
		remainMission = curRemainMission;

		gameObject.SetActive(true);
    }

    public void CancelMission()
	{
        gameObject.SetActive(false);
	}

	public void CompleteMission()
	{
		if (--remainMission <= 0)
		{
			UM.MissionClear(gameObject);

			// 한번 한 미션을 다시 하지 않도록. 플레이어들끼리 미션 공유하지 않음. 
			NM.Interactions[UM.curInteractionNum].SetActive(false);
		//	UM.MissionMaps[UM.curInteractionNum].SetActive(false);
		}
	}

}
