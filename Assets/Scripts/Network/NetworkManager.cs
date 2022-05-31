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
    public static NetworkManager NM = null;

    private void Awake()
    {
        if (NM == null)
        {
            NM = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject DisconnectPanel, WaitingPanel, InfoPanel, GamePanel,
           CrewWinPanel, ImposterWinPanel;
    public List<PlayerScript> Players = new List<PlayerScript>();
    public PlayerScript MyPlayer;

    public GameObject CrewInfoText, ImposterInfoText, WaitingBackground, Background;
    public bool isGameStart;
    public Transform SpawnPoint;
    public Light2D PointLight2D;
    public GameObject[] Interactions;
    public GameObject[] Doors;
    public GameObject[] Lights;
    PhotonView PV;
    public bool isTest;
    public enum ImpoType { OnlyMaster, Rand1, Rand2 }
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
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 10 }, null);
    }

    public override void OnJoinedRoom()
    {
        ShowPanel(WaitingPanel);
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

    //좀 의문이 드는 곳 - 강의들어야함. 
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
        ChatManager.CM.photonView.RPC("ChatClearRPC", RpcTarget.AllViaServer, false);

        PV.RPC("GameStartRPC", RpcTarget.AllViaServer);
    }


    //로봇사냥꾼 랜덤 설정
    void SetImpoCrew()
    {
        List<PlayerScript> GachaList = new List<PlayerScript>(Players);

        if (impoType == ImpoType.OnlyMaster)
        {
            Players[0].GetComponent<PhotonView>().RPC("SetImpoCrew", RpcTarget.AllViaServer, true);// 테스트 : 방장만 임포스터
        }

        else if (impoType == ImpoType.Rand1)
        {
            for (int i = 0; i < 1; i++) // 
            {
                int rand = Random.Range(0, GachaList.Count); // 랜덤
                Players[rand].GetComponent<PhotonView>().RPC("SetImpoCrew", RpcTarget.AllViaServer, true);
                GachaList.RemoveAt(rand);
            }
        }
    }
    //public void SeparateMission()
    //{
    //    //네트워크 매니져의 Players를 가져온다.(결국에는 PlayerScripts꺼. )
    //    List<PlayerScript> SeparateList = new List<PlayerScript>(Players);

    //    //다시 모든 플레이어들의 수를 가져온다. 

    //    for (int i = 0; i < Players.Count; i++)
    //    {
    //        Players[i].`

    //    }
    //}



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
        MyPlayer.SetPos(SpawnPoint.position); //캐릭터들을 스폰시키는 위치.
        MyPlayer.SetNickColor();
        //MyPlayer.SetMission();
        UM.GetComponent<PhotonView>().RPC("SetMaxMissionGage", RpcTarget.AllViaServer);

        yield return new WaitForSeconds(1);
        ShowPanel(GamePanel);
        ShowGameUI();
        StartCoroutine(UM.KillCo());
    }


    //기본적 UI구성
    public void ShowGameUI()
    {
        if (MyPlayer.isImposter)
        {
            UM.SetInteractionBtn1(0, false);
            UM.SetInteractionBtn2(5, false);
        }
        else
        {
            UM.SetInteractionBtn1(0, false);
            UM.SetInteractionBtn2(5, false);
        }
    }

    //모두 한곳으로 넣어버리는 GetCrewCount - UI 와 유기적으로 작동해야함. 
    public int GetCrewCount()
    {
        int crewCount = 0;
        for (int i = 0; i < Players.Count; i++)
            if (!Players[i].isImposter) ++crewCount;
        return crewCount;
    }







    //이 탈출해야지만, 승리하는 조건. 
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

        //if (impoCount == 0 && crewCount > 0) 
        //    Winner(true); //탐험로봇 승리
        Winner(true);
        
        if(impoCount != 0 && impoCount > crewCount) 
            Winner(false); //로봇청소기 승리
    }


    public void Winner(bool isCrewWin)
    {
        if (!isGameStart) return;

        if (isCrewWin)
        {
            print("탐험로봇 승리");
            ShowPanel(CrewWinPanel);
            Invoke("WinnerDelay", 3);
        }
        else
        {
            print("로봇청소기 승리");
            ShowPanel(ImposterWinPanel);
            Invoke("WinnerDelay", 3);
        }
    }

    void WinnerDelay()
    {
        PV.RPC("Winner", RpcTarget.AllViaServer);
        Application.Quit();
    }
}
