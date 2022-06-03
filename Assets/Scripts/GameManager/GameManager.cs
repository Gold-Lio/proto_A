using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public bool isGameStart
    {
        get;
        private set;
    }

    public bool isPlayerDead
    {
        get;
        set;
    }

    private void Update()
    {

    }
}
