using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    // Item 관련 --------------------------------------------------------------------------------
    private ItemDataManager itemData;
    public ItemDataManager ItemData
    {
        get => itemData;
    }
    // ------------------------------------------------------------------------------------------

    // Inven 관련 --------------------------------------------------------------------------------
    private InventoryUI inventoryUI;
    public InventoryUI InvenUI => inventoryUI;
    // ------------------------------------------------------------------------------------------

    // Player 관련 테스트 ------------------------------------------------------------------------
    private PlayerScript player;
    public PlayerScript MainPlayer => player;
    // ------------------------------------------------------------------------------------------

    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
                instance.Initialize();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }


    //주기적으로 자동 실행될 메서드 -현재 유물 진행도 여부
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {


    }


    private AudioSource audioSource;

    public bool isDead
    {
        get;
        private set;
    }

    public bool isGameEnd
    {
        get;
        private set;
    }
    public bool isGameStart
    {
        get;
        private set;
    }

    // 게임 오버 처리
    public void EndGame()
    {
        // 게임 오버 상태를 참으로 변경
        isGameEnd = true;
        // 게임 오버 UI를 활성화
        //UIManager.instance.SetActiveGameoverUI(true);
    }


    public override void OnLeftRoom()
    {
        //씬 매니저 로비씬. 
    }

    /// <summary>
    /// 게임매니저 초기화 (게임매니저가 재생성될때 Awake가 실행될때 컴포지트를 중복으로 불러올수 있어서 생성
    /// </summary>
    private void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();

        inventoryUI = FindObjectOfType<InventoryUI>();
    }
}
