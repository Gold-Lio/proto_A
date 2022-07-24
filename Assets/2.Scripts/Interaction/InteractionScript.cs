using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class InteractionScript : MonoBehaviourPun
{
    private InteractionScript IS;
    public enum Type { Customize, Mission, Altar_Box };
     public Type type;
    MinigameManager MM;
   // public static Item go;

    GameObject Line;
    public int curInteractionNum;

    public bool isAltarBox;

    void Start()
    {
        //	Line = transform.GetChild(0).gameObject;
        MM = GetComponent<MinigameManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine)
        {
            if (type == Type.Customize)
            {
                //Line.SetActive(true);
                UM.SetInteractionBtn0(1, true);
            }

            else if (type == Type.Mission)
            {
                UM.curInteractionNum = curInteractionNum;
                //Line.SetActive(true);
                UM.SetInteractionBtn0(0, true);
            }

            else if(type == Type.Altar_Box)
            {
                isAltarBox = true;
                UM.SetInteractionBtn0(2, true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine)
        {
            if (type == Type.Customize)
            {
                //Line.SetActive(false);
                UM.SetInteractionBtn0(0, false);
            }

            else if (type == Type.Mission)
            {
                //Line.SetActive(false);
                UM.SetInteractionBtn0(0, false);
            }

            else if (type == Type.Altar_Box)
            {
                isAltarBox = false;
                UM.SetInteractionBtn0(2, false);
            }
        }
    }

    public void StartMission()
    {
        throw new System.NotImplementedException();
    }

    public void CancelMission()
    {
        throw new System.NotImplementedException();
    }

    public void CompleteMission()
    {
        throw new System.NotImplementedException();
    }
}
