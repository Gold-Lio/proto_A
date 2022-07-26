using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarInteractable : MonoBehaviour
{
    [SerializeField] private GameObject AltarPanel;
    public void use(bool isActive)
    {
        AltarPanel.SetActive(isActive);
    }
}
