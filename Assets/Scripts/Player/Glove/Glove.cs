using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public enum State
{
    Ready,
    Attack,
    Reload
}

public class Glove : MonoBehaviourPun
{
    public State AttackState;

    PhotonView PV;
    
    [SerializeField]
    private float knockBackStrength;

    private Transform hitLocation;

    private AudioSource GloveAudioPlayer;
    public AudioClip attackClip;

    private void Awake()
    {
        GloveAudioPlayer = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, new Vector2(8, 0), new Color(0, 1, 0));
        
    }

    private void Fire()
    {
        if(AttackState == State.Ready)
        {
            Shot();
        }
    }
    private void Shot()
    {
        Vector2 hitpostion = Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(8, 0));
        if (hit.collider != null)
        {

            Debug.Log(hit.collider.name);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D RB = col.gameObject.GetComponent<Rigidbody2D>();
        if (RB != null)
        {

            Vector2 input = col.transform.position - transform.position;
            input.y = 0;

            RB.AddForce(input.normalized * knockBackStrength, ForceMode2D.Impulse);


        }
    }
}
   

//    //public float knockBackPower = 1000;
//    //public float knockDuration = 1;

//    [SerializeField]
//    private float knockBackStrength;
//    public float knockBackTime;
//    public ParticleSystem hitEffect;

//    public Transform hitEffectLocation;


//    private AudioSource hitAudio;
//    public AudioClip hitSound;

//    private void Awake()
//    {
//        if (hitEffectLocation == null)
//        {
//            hitEffectLocation = this.transform;
//        }
//    }

//    private void Start()
//    {
//        hitAudio = GetComponent<AudioSource>();
//        hitEffect.gameObject.transform.position = hitEffectLocation.position;
//    }

//    [PunRPC]
//    private void OnTriggerEnter2D(Collider2D col)
//    {
//        if (col.gameObject.CompareTag("Player"))
//        {
//            hitEffect?.Play();
//            hitAudio?.PlayOneShot(hitSound);
//            Rigidbody2D player = col.gameObject.GetComponent<Rigidbody2D>();
//            Debug.Log("들어옴");

//            if (player != null)
//            {
//                Vector2 difference = player.transform.position - transform.position;
//                player.AddForce(difference.normalized * knockBackStrength, ForceMode2D.Impulse);
//                Debug.Log("나감");
//                difference = difference.normalized * 4;
//                player.AddForce(difference, ForceMode2D.Impulse);
//                // player.isKinematic = true;
//                StartCoroutine(KnockBackCo(player));
//            }
//            //넉백에서 날라가는 위치 자체도 동기화가 필요하다.
//            //   StartCoroutine(PlayerScript.PS.KnockBack(knockDuration, knockBackPower, this.transform));
//        }
//    }


//    //private void OnTriggerEnter2D(Collider2D col)
//    //{
//    //    if (col.gameObject.CompareTag("IsPunch"))
//    //    {
//    //        Rigidbody2D player = col.GetComponent<Rigidbody2D>();

//    //        if (player != null)
//    //        {
//    //            player.isKinematic = false;
//    //            Vector2 difference = player.transform.position - transform.position;
//    //            difference = difference.normalized * 100;
//    //            player.AddForce(difference, ForceMode2D.Impulse);
//    //            // player.isKinematic = true;
//    //            StartCoroutine(KnockBackCo(player));
//    //        }
//    //    }
//    //}

//    [PunRPC]
//    private IEnumerator KnockBackCo(Rigidbody2D player)
//    {
//        if (player != null)
//        {
//            yield return new WaitForSeconds(knockBackTime);
//            player.velocity = Vector2.zero;
//            player.isKinematic = true;
//        }
//    }
//}


////Vector2 difference = transform.position - col.transform.position;
////transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);


