using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using UnityEngine.Experimental.Rendering.Universal;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager NM; // 싱글톤이 할당될 변수

    public void Awake() => NM = this;

    public GameObject DisconnectPanel, WaitingPanel, InfoPanel, GamePanel, CrewWinPanel, ImposterWinPanel;
    public List<PlayerScript> Players = new List<PlayerScript>();
    public PlayerScript MyPlayer;

    public Text timeText;
    public float time;
    private float selectCountdown;

    public GameObject studentInfoText, studentInfoText_S, badGuyInfoText, badGuyInfoText_S, WaitingBackground, Background;
    public GameObject onChatButton;

    public bool isGameStart;
    public bool isCrewWin;
    public Transform SpawnPoint;
    public Light2D PointLight2D;
    public GameObject[] Interactions;
    public GameObject[] Altars;
    public GameObject[] Lights;
    public GameObject[] wall;
    public Text pingText;


    PhotonView PV;
    public bool isTest;
    public enum ImpoType { Rand1 }
    public ImpoType impoType;

    void Start()
    {
        if (isTest) return;

        Screen.SetResolution(800, 400, false);
        PV = photonView;
        ShowPanel(DisconnectPanel);
        ShowBackground(WaitingBackground);

        InvokeRepeating("UpdatePing", 2, 2);
    }

    public void Connect(InputField NickInput)
    {
        if (string.IsNullOrWhiteSpace(NickInput.text)) return;
        PhotonNetwork.LocalPlayer.NickName = NickInput.text;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        ShowPanel(WaitingPanel);
        onChatButton.SetActive(true); //채팅활성화
        MyPlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity)
            .GetComponent<PlayerScript>();

        SetRandColor();
    }

    public void ShowPanel(GameObject CurPanel)
    {
        DisconnectPanel.SetActive(false);
        WaitingPanel.SetActive(false);
        InfoPanel.SetActive(false);
        GamePanel.SetActive(false);
        CrewWinPanel.SetActive(false);
        ImposterWinPanel.SetActive(false);

        CurPanel.SetActive(true);
    }

    void ShowBackground(GameObject CurBackground)
    {
        WaitingBackground.SetActive(false);
        Background.SetActive(false);

        onChatButton.SetActive(false); //채팅 비활성화
        CurBackground.SetActive(true);
    }

    void SetRandColor()
    {
        List<int> PlayerColors = new List<int>();
        for (int i = 0; i < Players.Count; i++)
            PlayerColors.Add(Players[i].colorIndex);

        while (true)
        {
            int rand = Random.Range(1, 13);
            if (!PlayerColors.Contains(rand))
            {
                MyPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, rand);
                break;
            }
        }
    }

    public void SortPlayers() => Players.Sort((p1, p2) => p1.actor.CompareTo(p2.actor));
    public Color GetColor(int colorIndex)
    {
        return UM.colors[colorIndex];
    }

    public void GameStart()
    {
        // 방장이 게임시작
        SetImpoCrew();
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        PV.RPC("GameStartRPC", RpcTarget.AllViaServer);
    }

    void SetImpoCrew()
    {
        List<PlayerScript> GachaList = new List<PlayerScript>(Players);

        if (impoType == ImpoType.Rand1)
        {
            for (int i = 0; i < 1; i++) //  파라오 1명 (테스트)
            {
                int rand = Random.Range(0, GachaList.Count); // 플레이어 에서 끌어와 랜덤
                Players[rand].GetComponent<PhotonView>().RPC("SetImpoCrew", RpcTarget.AllViaServer, true);
                GachaList.RemoveAt(rand);
            }
        }
    }

    [PunRPC]
    void GameStartRPC()
    {
        StartCoroutine(GameStartCo());
    }

    IEnumerator GameStartCo()
    {
        ShowPanel(InfoPanel);
        ShowBackground(Background);


        if (MyPlayer.isImposter)
        {
            badGuyInfoText.SetActive(true);
            badGuyInfoText_S.SetActive(true);
        }
        else
        {
            studentInfoText.SetActive(true);
            studentInfoText_S.SetActive(true);
        }


        yield return new WaitForSeconds(3);
        isGameStart = true;

        MyPlayer.SetPos(SpawnPoint.position);
        MyPlayer.SetNickColor();
        MyPlayer.SetMission();
        UM.GetComponent<PhotonView>().RPC("SetMaxMissionGage", RpcTarget.AllViaServer);

        ShowPanel(GamePanel);
        ShowGameUI();
        StartCoroutine(UM.PunchCoolCo());
        selectCountdown = time;
        StartCoroutine(LightCheckCo());
    }

    private void Update()
    {
        if (Mathf.Floor(selectCountdown) <= 0)
        {
            Winner(false); 
            // Count 0일때 동작할 함수 삽입
        }
        else
        {
            selectCountdown -= Time.deltaTime;
            timeText.text = Mathf.Floor(selectCountdown).ToString();
        }
    }

    void UpdatePing()
    {
        int pingRate = PhotonNetwork.GetPing();
        pingText.text = "Ping : " + pingRate;
    }

    public int GetCrewCount()
    {
        int crewCount = 0;
        for (int i = 0; i < Players.Count; i++)
            if (!Players[i].isImposter) ++crewCount;
        return crewCount;
    }

    void ShowGameUI()
    {
        if (MyPlayer.isImposter)
        {
            UM.SetInteractionBtn0(0, false); //첫번째 버튼이 use로 세팅
            UM.SetInteractionBtn1(5, true); //두번재 버튼이 킬로 세팅
        }
        else
        {
            UM.SetInteractionBtn0(0, false); //첫번째 버튼이 use로 세팅
            UM.SetInteractionBtn1(5, true); //두번재 버튼이 킬로 세팅   
        }
    }
     
    //학생과 악마 빛 범위 차이 
    IEnumerator LightCheckCo()
    {
        if(isGameStart)
        {
            if (MyPlayer.isImposter)
            {
                PointLight2D.pointLightOuterRadius = 90;
            }

            else
            {
                PointLight2D.pointLightOuterRadius = 50;
            }
        }
        yield return null;
    }


    [PunRPC]
    public void WinCheck()
    {
        int crewCount = 0;
        int impoCount = 0;

        for (int i = 0; i < Players.Count; i++)
        {
            var Player = Players[i];
            if (Players[i].isDie) continue;
            if (Player.isImposter)
                ++impoCount;
            else
                ++crewCount;
        }

        if (impoCount == 0 && crewCount > 0) // 모든 임포가 죽음
            Winner(true);
        else if (impoCount != 0 && impoCount > crewCount) // 임포가 크루보다 많음
            Winner(false);
    }

    public void Winner(bool isCrewWin)
    {
        if (!isGameStart) return;

        if (isCrewWin)
        {
            print("학생 승리");
            ShowPanel(CrewWinPanel);
            Invoke("WinnerDelay", 3);
        }
        else
        {
            print("악령 승리");
            ShowPanel(ImposterWinPanel);
            Invoke("WinnerDelay", 3);
        }
    }

    void WinnerDelay()
    {
        Application.Quit();
    }
}
