﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class InteractionScript : MonoBehaviourPun
{
	public enum Type { Customize, Mission, PickUp };
	public Type type;
	GameObject Line;
	public int curInteractionNum;
	private Animator anim;


	void Start()
    {		
	//	Line = transform.GetChild(0).gameObject;
		anim = GetComponent<Animator>();
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
				// if (col.GetComponent<PlayerScript>().isImposter) return;

				UM.curInteractionNum = curInteractionNum;
				//Line.SetActive(true);
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
				//Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}

			else if (type == Type.Mission)
			{
				//if (col.GetComponent<PlayerScript>().isImposter) return;

				//Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}
		}
	}


}
