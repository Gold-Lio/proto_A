using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;
using UnityEngine.UI;


public class InteractionScript : MonoBehaviourPun
{
    private InteractionScript IS;
    public enum Type { Customize, Mission, PickUp };
    public Type type;
    MinigameManager MM;
   // public static Item go;

    GameObject Line;
    public int curInteractionNum;

    public Button lastButton;

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

            else if (type == Type.PickUp)
            {
                UM.SetInteractionBtn2(6, true);

                //여기서 애드리스너 하고 
                // 여기서 col.디스트로이

                lastButton.onClick.AddListener(() => listner());
               
                // col.gameObject.SetActive(false);
                //Debug.Log(" 픽업 완료"); 혼자만 죽는다..
            }
        }
    }

    public void listner()
    {
        Debug.Log("리스너 잘 드러가유~~");
    }

    //애드리스너 함수를 넣어야한다. 





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

            else if (type == Type.PickUp)
            {
                UM.SetInteractionBtn2(6, false);

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
