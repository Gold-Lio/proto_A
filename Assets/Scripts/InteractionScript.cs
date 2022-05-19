using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class InteractionScript : MonoBehaviourPun
{
	public enum Type { Customize, Mission, Goldbox }
	public Type type;
	GameObject Line;
	public int curInteractionNum;

	void Start()
    {		
		Line = transform.GetChild(0).gameObject;
    }

	//인터렉션 Ontrigger이벤트  true 와 false로 정리되어있음. 
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
				//if (col.GetComponent<PlayerScript>().isImposter) return;

				UM.curInteractionNum = curInteractionNum;
				Line.SetActive(true);
				UM.SetInteractionBtn0(0, true);
			}

			else if(type == Type.Goldbox)
            {
				UM.curInteractionNum = curInteractionNum;
				Line.SetActive(true);
				UM.SetInteractionBtn0(0, true);
            }
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine) 
		{
			if (type == Type.Customize)
			{
				Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}

			else if (type == Type.Mission)
			{
				//if (col.GetComponent<PlayerScript>().isImposter) return;

				Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}

			else if (type == Type.Goldbox)
			{
				UM.curInteractionNum = curInteractionNum;
				Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}
		}
	}
}
