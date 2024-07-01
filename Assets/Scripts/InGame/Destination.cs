using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destination : MonoBehaviour
{
    string currentSceneName;


    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 다음 스테이지로 넘어가는 코드
            int currentStageNumber = int.Parse(currentSceneName.Substring(6));
            int nextStageNumber = currentStageNumber + 1;

            string nextSceneName = $"Stage_0{nextStageNumber}";
            //string nextScenePP = $"Stage0{nextStageNumber}"; // 플레이어프렙스는 Stage01이런식으로 저장

            if (nextStageNumber <= 8)
            {
                if (nextStageNumber == 5) SoundManager.Instance.TownBgmOn();
                SoundManager.Instance.EffectSoundOn("StageClear");
                SceneManager.LoadScene(nextSceneName);
                PlayerPrefs.SetInt(nextSceneName, 1);
            }
            else if (nextStageNumber == 9)
            {
                SoundManager.Instance.EffectSoundOn("StageClear");
                SceneManager.LoadScene("Ending");
                PlayerPrefs.SetInt("Ending", 1);
            }

            // 스테이지 전환시 페이드인 페이드 아웃 함수 삽입
        } 
        
    }
}
