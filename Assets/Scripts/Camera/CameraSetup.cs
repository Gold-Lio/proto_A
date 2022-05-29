using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // �ó׸ӽ� ���� �ڵ�
using Photon.Pun;

public class CameraSetup : MonoBehaviourPun
{
    // �ó׸ӽ� ī�޶� ���� �÷��̾ �����ϵ��� ����
    void Start()
    {
        // ���� �ڽ��� ���� �÷��̾���
        if (this.photonView.IsMine)
        {
            // ���� �ִ� �ó׸ӽ� ���� ī�޶� ã��
            var followCam = FindObjectOfType<CinemachineVirtualCamera>();
            // ���� ī�޶��� ���� ����� �ڽ��� Ʈ���������� ����
            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }
}