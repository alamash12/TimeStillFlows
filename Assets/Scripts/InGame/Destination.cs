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
    private void OnTriggerEnter2D(Collider2D collision) // 최종 목적지 도착 시 호출
    {
        if (collision.CompareTag("Player"))
        {
            // 다음 스테이지로 넘어가는 코드
            int currentStageNumber = int.Parse(currentSceneName.Substring(6));
            int nextStageNumber = currentStageNumber + 1;

            string nextSceneName = $"Stage_0{nextStageNumber}";
           
            if (nextStageNumber <= 8)
            {
                if (nextStageNumber == 5) SoundManager.Instance.TownBgmOn(); // 만약 스테이지 5로 넘어간다면 TownBgm재생

                SoundManager.Instance.EffectSoundOn("StageClear");
                SceneManager.LoadScene(nextSceneName);
                PlayerPrefs.SetInt(nextSceneName, 1); // PlayerPrefs의 다음 스테이지 Key를 1로 저장한다 -> 이어하기에서 플레이하기 위해
            }
            else if (nextStageNumber == 9) // 마지막 스테이지 클리어 후
            {
                SoundManager.Instance.EffectSoundOn("StageClear");
                SceneManager.LoadScene("Ending");
                PlayerPrefs.SetInt("Ending", 1);
            }
        } 
    }
}
