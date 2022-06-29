using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShockwaveListener : MonoBehaviour
{

    private CinemachineImpulseSource source;

    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        InvokeRepeating("Shake", 3f, 4f);
    }

    public void Shake()
    {
        source.GenerateImpulse();
    }

}
