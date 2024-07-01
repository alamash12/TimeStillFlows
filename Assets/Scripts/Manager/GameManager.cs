using System;
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
        Application.targetFrameRate = 60;
    }

    static public void ChangeSprite(SpriteRenderer spriteRenderer, int count) // count는 enum값 변경을 위한 매개변수
    {
        string spriteName = spriteRenderer.sprite.name;
        string spritePath = spriteName.Split('_')[0]; // 폴더를 지정해주기 위한 스트링
        SpriteDefine.ObjectSprite result;
        if (Enum.TryParse(spriteName, out result)) // 스프라이트의 이름을 enum값으로 변환후 result에 저장
        {
            SpriteDefine.ObjectSprite newResult = (SpriteDefine.ObjectSprite)((int)result + count);
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + spritePath + "/" + newResult.ToString());
        }
    }
}