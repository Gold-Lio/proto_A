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
    public float slowTime;

    Vector2 camPosition_original;
    public float shake;


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

            if (RB != null)
            {
               // TimeStop();
                Debug.Log("Ÿ�ӽ�ž");
                CinemachineShake.Instance.ShakeCamera(camShakeIntencity, camShakeTime);
                //col.GetComponent<PlayerScript>();
                //Vector2 input = col.transform.position - transform.position;
                //input.y = 0;
                //RB.AddForce(input.normalized * knockBackStrength, ForceMode2D.Impulse);
                ////PlayerScript.PS.TimeStop();
                //Debug.Log("OK");
                //StartCoroutine(CamAction());
            }
        }
    }

    //public void TimeStop()
    //{
    //    if (!stopping)
    //    {
    //        stopping = true;
    //        Time.timeScale = 0;

    //        StartCoroutine("Stop");
    //        StartCoroutine("CamAction");
    //    }
    //}

    //IEnumerator Stop()
    //{
    //    yield return new WaitForSecondsRealtime(stopTime);
    //    Time.timeScale = 0.01f;
    //    yield return new WaitForSecondsRealtime(slowTime);

    //    Time.timeScale = 1;
    //    stopping = false;
    //}

    //IEnumerator CamAction()
    //{

    //    camPosition_original = cam.position;

    //    cam.position =
    //        new Vector2(cam.position.x + Random.Range(-shake, shake), cam.position.y + Random.Range(-shake, shake));
    //    yield return new WaitForSecondsRealtime(0.05f);

    //    cam.position =
    //              new Vector2(cam.position.x + Random.Range(-shake, shake), cam.position.y + Random.Range(-shake, shake));
    //    yield return new WaitForSecondsRealtime(0.05f);

    //    cam.position = camPosition_original;
    //}


    //public static CameraSetup instance;
    //public CinemachineVirtualCamera followCam;
    //// �ó׸ӽ� ī�޶� ���� �÷��̾ �����ϵ��� ����
    //void Start()
    //{
    //    // ���� �ڽ��� ���� �÷��̾���
    //    if (this.photonView.IsMine)
    //    {
    //        // ���� �ִ� �ó׸ӽ� ���� ī�޶� ã��
    //        followCam = FindObjectOfType<CinemachineVirtualCamera>();
    //        // ���� ī�޶��� ���� ����� �ڽ��� Ʈ���������� ����
    //        followCam.Follow = transform;
    //        followCam.LookAt = transform;
    //    }
    //}


    [PunRPC]
    void DirRPC(int dir) => this.dir = dir;


    // IEnumerator CamAction()
    // {
    ////     cam = CameraSetup.instance.transform;

    //     camPosition_Origin = cam.position;
    //     cam.position = 
    //         new Vector3(cam.position.x + Random.Range(-shake,shake),cam.position.y + Random.Range(-shake,shake),
    //         cam.position.z + Random.Range(-shake,shake));
    //     yield return new WaitForSecondsRealtime(0.05f);
    //     cam.position =
    //               new Vector3(cam.position.x + Random.Range(-shake, shake), cam.position.y + Random.Range(-shake, shake),
    //               cam.position.z + Random.Range(-shake, shake));
    //     yield return new WaitForSecondsRealtime(0.05f);

    //     cam.position = camPosition_Origin;
    // }
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
//            Debug.Log("����");

//            if (player != null)
//            {
//                Vector2 difference = player.transform.position - transform.position;
//                player.AddForce(difference.normalized * knockBackStrength, ForceMode2D.Impulse);
//                Debug.Log("����");
//                difference = difference.normalized * 4;
//                player.AddForce(difference, ForceMode2D.Impulse);
//                // player.isKinematic = true;
//                StartCoroutine(KnockBackCo(player));
//            }
//            //�˹鿡�� ���󰡴� ��ġ ��ü�� ����ȭ�� �ʿ��ϴ�.
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


