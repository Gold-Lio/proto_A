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
        MM = GetComponent<MinigameManager>();
    }

    private void OnEnable()
    {
        isClear = false;
    }

    public void AddButton()
    {
        slider.value += 0.1f;

        if(!isClear)
        {
            slider.value = 1; // Value 최고치에 달했을경우
        
            MissionClear();
        }
    }


    void MissionClear()
    {
        isClear = true;
        MM.CompleteMission();
    }

}
