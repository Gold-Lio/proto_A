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
    private CharacterController characterController; // �̰� ���ľ���. 

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        maxStamina = stamina;
        stText.text = ((int)(stamina / maxStamina * 100f)).ToString() + "%";
        stBar.localScale = Vector3.one;

    }

    //Update������ ó�� ���ִ°�.  if( ������ ��쿡 � �ۼ�Ʈ�� ��������. 

    

}
