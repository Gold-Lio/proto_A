using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamZone : MonoBehaviour
{
    private CinemachineVirtualCamera VCamera = null;

    private void Start()
    {
        VCamera.enabled = false;
    }

    //움직이고 있다면 가까운 카메라 true.
    //움직임 입력이 없다면 가까운 카메라 false.

}
