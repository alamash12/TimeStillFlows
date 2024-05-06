using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameOption : MonoBehaviour
{
    public GameObject optionPanel; // 옵션 패널
    //public GameObject outsideOptionPanel; // 옵션 바깥패널

    public void ToggleOptionPanel()// 옵션버튼을 누를 시 옵션 패널을 띄움.
    {
        if (!optionPanel.activeSelf) optionPanel.SetActive(true);
        //if (!outsideOptionPanel.activeSelf) outsideOptionPanel.SetActive(true);
        Time.timeScale = 0.0f; // 게임을 멈춤
    }
    public void ResumeButton()// 옵션 바깥패널을 누를 시 옵션 패널을 없애고 게임을 진행.
    {
        optionPanel.SetActive(false);
        //outsideOptionPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void RetryButton()
    {
        Time.timeScale = 1.0f;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
