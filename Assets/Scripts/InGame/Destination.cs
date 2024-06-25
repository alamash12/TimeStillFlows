using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destination : MonoBehaviour
{
    string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // 다음 스테이지로 넘어가는 코드
            switch(sceneName)
            {
                case "Stage01":
                    // 스킬 해금
                    // 씬 이동
                    // PlayerPrefs.SetInt("SkillUnlocked", 변수명 ? 1 : 0); 이런식으로 bool 표현가능
                    break;
                case "Stage02":
                    // 스킬 해금
                    // 씬 이동
                    break;
                case "Stage03":
                    // 스킬 해금
                    // 씬 이동
                    break;
                default :
                    // 씬 이동
                    break;
            }
            // 스테이지 전환시 페이드인 페이드 아웃 함수 삽입
        }
    }
}
