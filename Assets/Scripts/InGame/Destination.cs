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
                    // 씬 이동
                    SceneManager.LoadScene("Stage02");
                    // PlayerPrefs.SetInt("SkillUnlocked", 변수명 ? 1 : 0); 이런식으로 bool 표현가능
                    //PlayerPrefs.SetInt("Stage01", 1);
                    PlayerPrefs.SetInt("Stage02", 1);
                    break;
                case "Stage02":
                    // 씬 이동
                    SceneManager.LoadScene("Stage03");
                    PlayerPrefs.SetInt("Stage03", 1);
                    break;
                case "Stage03":
                    // 씬 이동
                    SceneManager.LoadScene("Stage04");
                    PlayerPrefs.SetInt("Stage04", 1);
                    break;
                case "Stage04":
                    SceneManager.LoadScene("Stage05");
                    PlayerPrefs.SetInt("Stage05", 1);
                    break;
                case "Stage05":
                    SceneManager.LoadScene("Stage06");
                    PlayerPrefs.SetInt("Stage06", 1); break;
                case "Stage06":
                    SceneManager.LoadScene("Stage07");
                    PlayerPrefs.SetInt("Stage07", 1); break;
                case "Stage07":
                    SceneManager.LoadScene("Stage08");
                    PlayerPrefs.SetInt("Stage08", 1); break;
                case "Stage08":
                    SceneManager.LoadScene("Ending");
                    break;
                default :
                    // 씬 이동
                    break;
            }
            // 스테이지 전환시 페이드인 페이드 아웃 함수 삽입
        }
    }
}
