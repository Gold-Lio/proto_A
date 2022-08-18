using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini3Target : MonoBehaviour
{

    [SerializeField] private Slider slider;
    MinigameManager MM;
    bool isClear = false;

    private void Start()
    {
        MM = transform.GetComponentInParent<MinigameManager>();
    }

    private void OnEnable()
    {
        isClear = false;
        slider.value = 0;
        slider.interactable = true;
    }

    public void AddButton()
    {
        slider.value += 1f;

        if(!isClear)
        {
            if(slider.value == slider.maxValue) // Value 최고치에 달했을경우
            MissionClear();
        }
    }

    void MissionClear()
    {
        isClear = true;
        MM.CompleteMission();
    }
}
