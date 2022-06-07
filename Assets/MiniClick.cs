using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MiniClick : MonoBehaviour
{
    public Slider slider;
    private MinigameManager MM;

    bool isClear;
    private void Start()
    {
        isClear = false;
        MM = transform.GetComponentInParent<MinigameManager>();
    }

    private void OnEnable()
    {
        isClear = false;
        slider.value = 0.0f; 
    }

    public void OnButton()
    {
        slider.value += 0.03f;
        if(slider.value == slider.maxValue)
        {
            if (!isClear) MissionClear();
        }
    }

    private void MissionClear()
    {
        isClear = true;
        MM.CompleteMission();
    }

}
