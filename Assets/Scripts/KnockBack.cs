using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockBackTime;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("IsPunch"))
        {
            Debug.Log("Player Detacted");
            Rigidbody2D player = col.GetComponent<Rigidbody2D>();

            if (player != null)
            {
                player.isKinematic = false;

                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * 4;
                player.AddForce(difference, ForceMode2D.Impulse);
                player.isKinematic = true;

                StartCoroutine(KnockBackCo(player));
            }
        }
    }

    private IEnumerator KnockBackCo(Rigidbody2D player)
    {
        if (player != null)
        {
            yield return new WaitForSeconds(knockBackTime);
            player.velocity = Vector2.zero;
            player.isKinematic = true;
        }
    }
}


//Vector2 difference = transform.position - col.transform.position;
//transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);


