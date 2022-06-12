using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.GetComponent<PlayerScript>())
        {
            Debug.Log("Player Detacted");
            Vector2 difference = transform.position - col.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
            //Rigidbody2D player = col.GetComponent<Rigidbody2D>();

            //if (player != null)
            //{
            //    player.isKinematic = false;

            //    Vector2 difference =  player.transform.position - transform.position;
            //    difference = difference.normalized * thrust;
            //    player.AddForce(difference, ForceMode2D.Impulse);
            //    player.isKinematic = true;
            //}
        }
    }

}
