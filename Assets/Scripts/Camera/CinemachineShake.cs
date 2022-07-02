using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; } 
    private CinemachineVirtualCamera CV;

    private float shakerTimer;
    private float shakeTimerTotal;
    private float startingIntencity;

    private void Awake()
    {
        Instance = this; 
        CV = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin CBMCP = CV.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        CBMCP.m_AmplitudeGain = intensity;
        
        startingIntencity = intensity;
        shakeTimerTotal = time;
        shakerTimer = time;
    }


    private void Update()
    {
        if(shakerTimer > 0f)
        {
            shakerTimer -= Time.deltaTime;
            if(shakerTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin CBMCP = CV.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                CBMCP.m_AmplitudeGain = 0f;
            }
        }
    }
}
