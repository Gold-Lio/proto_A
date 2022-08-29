using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameDevTV.Core.UI.Dragging;
using Photon.Pun;
using Photon.Realtime;

namespace GameDevTV.UI.Inventories
{
    public class InventorySlotUI : MonoBehaviourPun, IDragContainer<Sprite>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;

        // PUBLIC

        [PunRPC]
        public int MaxAcceptable(Sprite item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        [PunRPC]
        public void AddItems(Sprite item, int number)
        {
            icon.SetItem(item);
        }

        [PunRPC]
        public Sprite GetItem()
        {
            return icon.GetItem();
        }

        [PunRPC]
        public int GetNumber()
        {
            return 1;
        }

        [PunRPC]
        public void RemoveItems(int number)
        {
            icon.SetItem(null);
        }
    }
}