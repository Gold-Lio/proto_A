using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class InteractionScript : MonoBehaviourPun
{
	public enum MissionType { Customize, Mission, locker };
	public MissionType missionType;

	public enum ItemType { Treasure }
	public ItemType itemType;


	GameObject Line;
	public int curInteractionNum;
	private Animator anim;


	void Start()
    {		
		Line = transform.GetChild(0).gameObject;
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine) 
		{
			if (missionType == MissionType.Customize)
			{
				Line.SetActive(true);
				UM.SetInteractionBtn0(1, true);
			}

			else if (missionType == MissionType.Mission)
			{
				// if (col.GetComponent<PlayerScript>().isImposter) return;

				UM.curInteractionNum = curInteractionNum;
				Line.SetActive(true);
				UM.SetInteractionBtn0(0, true);
			}

			//유물보관함
			else if(missionType == MissionType.locker)
            {
				UM.SetInteractionBtn0(0, true);
			}

			//유물
			if(itemType == ItemType.Treasure)
            {
				UM.SetInteractionBtn2(2, true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine) 
		{
			if (missionType == MissionType.Customize)
			{
				Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}

			else if (missionType == MissionType.Mission)
			{
				//if (col.GetComponent<PlayerScript>().isImposter) return;

				Line.SetActive(false);
				UM.SetInteractionBtn0(0, false);
			}

			//유물보관함
			else if (missionType == MissionType.locker)
			{
				UM.SetInteractionBtn0(0, false);
			}

			//유물
			if (itemType == ItemType.Treasure)
			{
				UM.SetInteractionBtn2(2, false);
			}

		}
	}


}
