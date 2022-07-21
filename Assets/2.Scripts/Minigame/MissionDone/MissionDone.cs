using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    Red,
    Green,
    Blue,
}

public class MissionDone : MonoBehaviour
{
    public MissionType missionType;

    private void Update()
    {
        CheckMission();
    }

    public void CheckMission()
    {
        if(missionType == MissionType.Red)
        {
            
        }

        if (missionType == MissionType.Green)
        {

        }

        if (missionType == MissionType.Blue)
        {

        }
    }
}
