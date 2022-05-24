using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float stamina = 1000f;
    private float maxStamina;
    private float minStamina;

    public Text stText;
    public RectTransform stBar;
    private CharacterController characterController; // 이거 고쳐야함. 

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        maxStamina = stamina;
        stText.text = ((int)(stamina / maxStamina * 100f)).ToString() + "%";
        stBar.localScale = Vector3.one;

    }

    //Update문에서 처리 해주는것.  if( 움직일 경우에 어떤 퍼센트로 움직일지. 

    

}
