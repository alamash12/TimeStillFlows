using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            Init();
            return instance;
        }
    }
    static void Init() // 다른 매니저들도 여기서 GameManager 객체에 추가
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (instance == null)
        {
            if (gameManager == null)
            {
                gameManager = new GameObject("GameManager");
                gameManager.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(gameManager);
            instance = gameManager.GetComponent<GameManager>();
        }
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            Init();
        }
    }


}