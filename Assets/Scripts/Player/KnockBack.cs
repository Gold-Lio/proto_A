using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class KnockBack : MonoBehaviourPunCallbacks
{
    public float knockBackStrength;

    public PhotonView PV;

    //ī�޶�
    public float camShakeIntencity;
    public float camShakeTime;

    int dir;

    bool stopping;
    public float stopTime;

    // private void Start() => Destroy(gameObject, 0.4f);
    private void Start()
    {
        Destroy(gameObject, 0.4f);
    }

    void Update() => transform.Translate(Vector3.right * 4f * Time.deltaTime * dir);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!PV.IsMine && col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine)
        {
            Rigidbody2D RB = col.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log("��´�");

            //if (RB != null)
            
                // TimeStop();
                CinemachineShake.Instance.ShakeCamera(camShakeIntencity, camShakeTime);
                col.GetComponent<PlayerScript>();

            
        }
    }
    
    [PunRPC]
    void DirRPC(int dir) => this.dir = dir;
    //
    // public void TimeStop()
    // {
    //     if (!stopping)
    //     {
    //         stopping = true;
    //         Time.timeScale = 0;
    //
    //         StartCoroutine(Stop());
    //     }
    // }
    //
    // IEnumerator Stop()
    // {
    //     yield return new WaitForSecondsRealtime(stopTime);
    //
    //     Time.timeScale = 1;
    //     stopping = false;
    // }
}