using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using UnityEngine.Experimental.Rendering.Universal;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager NM;
    void Awake() => NM = this;

    public GameObject DisconnectPanel, WaitingPanel, InfoPanel, GamePanel, CrewWinPanel, ImposterWinPanel;
    public List<PlayerScript> Players = new List<PlayerScript>();
    public PlayerScript MyPlayer;

    public Text timeText;
    public float time;
    private float selectCountdown;


    public GameObject CrewInfoText, ImposterInfoText, WaitingBackground, Background;
    public GameObject onChatButton;

    public bool isGameStart;
    public Transform SpawnPoint;
    public Light2D PointLight2D;
    public GameObject[] Interactions;
    public GameObject[] Lights;
    PhotonView PV;
    public bool isTest;
    public enum ImpoType { Rand1 }
    public ImpoType impoType;

    void Start()
    {
        if (isTest) return;

        Screen.SetResolution(720, 405, false);
        PV = photonView;
        ShowPanel(DisconnectPanel);
        ShowBackground(WaitingBackground);

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
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 5 }, null);
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
        if (MyPlayer.isImposter) ImposterInfoText.SetActive(true);
        else CrewInfoText.SetActive(true);

        yield return new WaitForSeconds(3);
        isGameStart = true;

        MyPlayer.SetPos(SpawnPoint.position);
        MyPlayer.SetNickColor();
        MyPlayer.SetMission();
        MyPlayer.SetHPBar();    // HPBar 추가
        UM.GetComponent<PhotonView>().RPC("SetMaxMissionGage", RpcTarget.AllViaServer);

        //  PlayerScript.PS.GetComponent<Inventory>();
        // Inventory.instance.SetInventory();

        ShowPanel(GamePanel);
        ShowGameUI();

        //Pickup.instance.SetPickup();
        //Slot.instance.SetSlot();

        //   Inventory_UI.instance.GoInventory()
        // Inventory.Instance.SetSlots();
        StartCoroutine(UM.PunchCoolCo());

        selectCountdown = time;

    }

    private void Update()
    {
        if (Mathf.Floor(selectCountdown) <= 0)
        {
            WinCheck();
            // Count 0일때 동작할 함수 삽입
        }
        else
        {
            selectCountdown -= Time.deltaTime;
            timeText.text = Mathf.Floor(selectCountdown).ToString();
        }
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
            UM.SetInteractionBtn2(6, false);
            UM.SetHPBar();
        }
        else
        {
            UM.SetInteractionBtn0(0, false); //첫번째 버튼이 use로 세팅
            UM.SetInteractionBtn1(5, true); //두번재 버튼이 킬로 세팅   
            UM.SetInteractionBtn2(6, false);
            UM.SetHPBar();
        }
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
            print("고고학자 승리");
            ShowPanel(CrewWinPanel);
            Invoke("WinnerDelay", 3);
        }
        else
        {
            print("파라오 승리");
            ShowPanel(ImposterWinPanel);
            Invoke("WinnerDelay", 3);
        }
    }

    void WinnerDelay()
    {
        Application.Quit();
    }
}
