using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class InteractionScript : MonoBehaviourPun
{
    public enum Type { Customize, Mission, Worthy, EndGameChenck };
    public Type type;
    GameObject Line;
    MinigameManager MM;
    PlayerScript PS;

    bool isEnded;

    public int curInteractionNum;

    void Start()
    {
        Line = transform.GetChild(0).gameObject;

        UM.SetInteractionBtn0(0, false);
        UM.SetInteractionBtn1(0, false);
        UM.SetInteractionBtn2(5, false);
    }

    public void MissionFail(GameObject MissionPanel)
    {
        MissionPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine)
        {
            if (type == Type.Customize)
            {
                Line.SetActive(true);
                UM.SetInteractionBtn0(1, true);
            }

            else if (type == Type.Mission)
            {
                UM.curInteractionNum = curInteractionNum;
                Line.SetActive(true);
                UM.SetInteractionBtn1(0, true);
            }

            else if (type == Type.Worthy) // 현재 보물상자는 미션진행 불가능   
            {
                Line.SetActive(true);
                UM.SetInteractionBtn1(0, true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine)
        {
            //isEnded = GetComponent<PlayerScript>().isImposter;

            if (type == Type.Customize)
            {
                Line.SetActive(false);
                UM.SetInteractionBtn0(0, false);
            }

            else if (type == Type.Mission)
            {
                Line.SetActive(false);
                UM.SetInteractionBtn1(0, false);
              //  MissionFail(gameObject.SetActive(false));
                 //현재 켜져있는 미션창도 꺼주세요
            }

            else if (type == Type.Worthy)  // 현재 보물상자는 미션진행 불가능   
            {
                Line.SetActive(false);
                UM.SetInteractionBtn1(0, false);
            }
            //미션 매니저꺼를 가져와서 해제???
            //여기서 캐릭터가 일정 수준 떨어질 경우.  -모든 미션창 취소되고 처음 으로 돌아가도록. 
        }
        if(col.CompareTag("Player"))
        {
            Line.SetActive(false);
            MM.CancelMission();
        }
    }
}

