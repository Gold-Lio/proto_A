using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // �ó׸ӽ� ���� �ڵ�
using Photon.Pun;
public class CameraSetup : MonoBehaviourPun
{
    public static CameraSetup instance;
    public CinemachineVirtualCamera followCam;
    void Start()
    {
        if (this.photonView.IsMine)
        {
            followCam = FindObjectOfType<CinemachineVirtualCamera>();
            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }
}
