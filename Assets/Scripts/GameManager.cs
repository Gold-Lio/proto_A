using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private static GameManager s_instance; // 유일성이 보장된다.
    private static GameManager Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate(); //입력장치에 접근
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<GameManager>();
        }
    }
}
