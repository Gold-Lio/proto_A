using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;
using Photon.Pun;
using Photon.Realtime;

public class ItemPickup : MonoBehaviourPun
{
    public int itemID;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine)
        {
            UM.SetInteractionBtn2(6, true);
        }
    }







}
