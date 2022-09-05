using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public partial class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    // Item ���� --------------------------------------------------------------------------------
    private ItemDataManager itemData;
    public ItemDataManager ItemData
    {
        get => itemData;
    }
    // ------------------------------------------------------------------------------------------

    // Inven ���� --------------------------------------------------------------------------------
    private InventoryUI inventoryUI;
    public InventoryUI InvenUI => inventoryUI;
    // ------------------------------------------------------------------------------------------

    // Player ���� �׽�Ʈ ------------------------------------------------------------------------
    private PlayerScript player;
    public PlayerScript MainPlayer => player;
    // ------------------------------------------------------------------------------------------

    public static GameManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
                instance.Initialize();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance; // �̱����� �Ҵ�� static ����

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
    }


    //�ֱ������� �ڵ� ����� �޼��� -���� ���� ���൵ ����
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

    // ���� ���� ó��
    public void EndGame()
    {
        // ���� ���� ���¸� ������ ����
        isGameEnd = true;
        // ���� ���� UI�� Ȱ��ȭ
        //UIManager.instance.SetActiveGameoverUI(true);
    }


    public override void OnLeftRoom()
    {
        //�� �Ŵ��� �κ��. 
    }

    /// <summary>
    /// ���ӸŴ��� �ʱ�ȭ (���ӸŴ����� ������ɶ� Awake�� ����ɶ� ������Ʈ�� �ߺ����� �ҷ��ü� �־ ����
    /// </summary>
    private void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();

        inventoryUI = FindObjectOfType<InventoryUI>();
    }
}
