using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace GameDevTV.UI.Inventories
{
    /// <summary>
    /// To be put on the icon representing an inventory item. Allows the slot to
    /// update the icon and number.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class InventoryItemIcon : MonoBehaviourPun
    {
        // PUBLIC

        [PunRPC]
        public void SetItem(Sprite item)
        {
            var iconImage = GetComponent<Image>();
            if (item == null)
            {
                iconImage.enabled = false;
            }
            else
            {
                iconImage.enabled = true;
                iconImage.sprite = item;
            }
        }

        [PunRPC]
        public Sprite GetItem()
        {
            var iconImage = GetComponent<Image>();
            if (!iconImage.enabled)
            {
                return null;
            }
            return iconImage.sprite;
        }
    }
}