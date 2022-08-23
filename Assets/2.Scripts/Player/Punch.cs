using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class Punch : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public float attackDamage;
    public PhotonView PV;
   // public PlayerHealth PH;
    
    public float camShakeIntencity;
    public float camShakeTime;

    int dir;

    bool stopping;
    public float stopTime;
    public float slowTime;

    Vector2 camPosition_original;
    public float shake;

    private void Start()
    {
        Destroy(gameObject, 0.7f);
    }

    void Update() => transform.Translate(Vector3.right * (3f * Time.deltaTime * dir));

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!PV.IsMine && col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine) // 느린쪽 판정
        {
            CinemachineShake.Instance.ShakeCamera(camShakeIntencity, camShakeTime);

            col.GetComponent<PlayerHealth>().Hit();
            PV.RPC("DestoryRPC", RpcTarget.AllBuffered);

            //col.GetComponent<PlayerHealth>().hp -= attackDamage;
            
            //col.GetComponent<PlayerScript>().anim.SetTrigger("Hited");
            //Debug.Log("때렸다");


           //if(col.GetComponent<PlayerHealth>().hp <= 0)
           //{
           //     // PV.RPC("PlayerDead", RpcTarget.AllBuffered,col);
           //     // Destroy(col.gameObject);
           //     //PlayerDie(col);
           //     PV.RPC("PlayerDie", RpcTarget.AllBuffered, col);
           //     PhotonNetwork.Instantiate("PlayerDeadStone", transform.position, Quaternion.identity);
           //}
        }
    }



    [PunRPC]
    public void PlayerDie(Collider2D col)
    {
        Destroy(col.gameObject);
    }

    [PunRPC]
    void DirRPC(int dir) => this.dir = dir;

    [PunRPC]
    void DestoryRPC() => Destroy(gameObject);
}