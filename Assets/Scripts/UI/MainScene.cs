using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene: MonoBehaviour
{
    /*
     * MainScene : 메인화면에서 일어나는 씬의 전환과 각종 상호작용들을 저장하는 클래스입니다.
     */
    public void StartNewGame() // NewGame을 누를 시 Intro화면으로 씬을 전환함.
    {
        SceneManager.LoadScene("Intro"); 
    }
    public void ContinueGame() // Continue를 누를 시 StageSelect화면으로 씬을 전환함.
    {
        SceneManager.LoadScene("StageSelect"); 
    }
    public void GameQuit() // Exit을 누를 시 어플리케이션 종료
    {
        Application.Quit();
    }

    /*
     * 옵션 패널관련
     */
    public GameObject optionPanel; // 옵션 패널
    
    public void ToggleOptionPanel()// 옵션버튼을 누를 시 옵션 패널과 옵션 바깥패널을 띄움
    {
        if(!optionPanel.activeSelf) optionPanel.SetActive(true);
    }
    public void CloseOptionPanel()// 옵션 바깥패널을 누를 시 옵션 패널과 옵션 바깥패널을 없앰
    {
        optionPanel.SetActive (false);
    }
    public void OptionIntroBtn()
    {
        SceneManager.LoadScene("Intro");
    }
}
