using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MiniDelay : MonoBehaviour // ���߿� �߰�.
{
    MinigameManager MM;
    bool isClear;

    [SerializeField]
    private float miniTime = 10f;

    private float curTime = 0f;
    public GameObject succeceGameObj;

    private Vector3 succescePos = new Vector3();

    private bool isPlay;

    private void Awake()
    {
        MM= transform.GetComponentInChildren<MinigameManager>();
    }

    //�̼� ������ �ʱ�ȭ
    private void OnEnable()
    {
        isClear = false;
    }


    private void Update()
    {
        isDelay();
    }

    private void isDelay()
    {
        if(isPlay)
        {
            if (curTime < miniTime)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                //succescePos = transform.position(new Vector3())
                //succeceGameObj.SetActive(true);

            }
        }
    }


}
