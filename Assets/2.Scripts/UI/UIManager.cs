using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;

public class UIManager : MonoBehaviourPun
{
    public static UIManager UM
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    private void Awake()
    {
        if (UM != this)
        {
            Destroy(gameObject);
        }
    }


  

    public Image PreviewImage;
    public Color[] colors;
    public GameObject CustomizePanel, DiePanel;
    public GameObject[] ColorCancel;
    public Button[] ColorBtn;
    public Button StartBtn;
    public Text LogText;

    PhotonView PV;

    public InputField ChatInput;
    public Text ChatText;
    public Scrollbar ChatScroll;
    public RectTransform ChatContent;
    public GameObject[] ChatPanels;

    public Animator anim;
    public int killCooltime;

    public GameObject playerDeathMenu;
    public Text deathText;

    void Start()
    {
        PV = photonView;
    }

    public void ColorChange(int _colorIndex)
    {
        PreviewImage.color = colors[_colorIndex];
        NM.MyPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, _colorIndex);
    }

    void Update()
    {
        if (!PhotonNetwork.InRoom) return;
        //SetActiveColors();
        if (!PhotonNetwork.IsMasterClient) return;
        ShowStartBtn();
    }

    void ShowStartBtn()
    {
        StartBtn.gameObject.SetActive(true);
        //StartBtn.interactable = PhotonNetwork.CurrentRoom.PlayerCount >= 7; // 기본값
        StartBtn.interactable = PhotonNetwork.CurrentRoom.PlayerCount >= 1; // 2
    }

    //public void SetActiveColors()
    //{
    //    List<int> colorList = new List<int>();
    //    for (int i = 0; i < NM.Players.Count; i++)
    //        colorList.Add(NM.Players[i].colorIndex);

    //    for (int i = 0; i < ColorCancel.Length; i++)
    //    {
    //        bool contain = colorList.Contains(i + 1);
    //        ColorCancel[i].SetActive(contain);
    //        ColorBtn[i].interactable = !contain;
    //    }
    //}

    //쿨타임
    //public IEnumerator PunchCoolCo()    
    //{
    //    SetInteractionBtn1(5, false);
    //    NM.MyPlayer.ispunch = false;

    //    for (int i = 10; i > 0; i--) // 기본 10초 킬대기
    //    {
    //        killCooltime = i;

    //        if (UM.curBtn1 == 5)
    //            Interaction1Text.text = killCooltime.ToString();
    //        else
    //            Interaction1Text.text = "";

    //        yield return new WaitForSeconds(1);
    //    }

    //    killCooltime = 0;
    //    Interaction1Text.text = "";
    //    //Enum상태 변경
    //    NM.MyPlayer.ispunch = true;
    //    SetInteractionBtn1(5, true);
    //}
}
