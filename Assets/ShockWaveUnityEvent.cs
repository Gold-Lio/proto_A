using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
public class ShockWaveUnityEvent : MonoBehaviour
{

    public UnityEvent shock;

    private void Start()
    {
        InvokeRepeating("ShockwaveEvent", 3f, 4f);
    }

    private void ShockwaveEvent()
    {
        shock.Invoke();
    }


}
