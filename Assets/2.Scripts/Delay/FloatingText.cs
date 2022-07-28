using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingText : MonoBehaviour
{
    public float moveSpeed;
    public float destoryTime;

    public Text text;

    private Vector3 vetor;

    private void Update()
    {
        vetor.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime), text.transform.position.z);
        text.transform.position = vetor;

        destoryTime -= Time.deltaTime;

        if(destoryTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }



}
