using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    /*
     * MainSceneManager : 메인화면에서 일어나는 씬의 전환과 각종 상호작용들을 저장하는 클래스입니다.
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
    public GameObject outsideOptionPanel; // 옵션 바깥패널
    /*public void Start()
    {
        optionPanel.SetActive(false); // 게임 시작시 옵션패널을 비활성화
        outsideOptionPanel.SetActive(false); // 게임 시작시 옵션 바깥패널을 비활성화
    }*/
    public void ToggleOptionPanel()// 옵션버튼을 누를 시 옵션 패널과 옵션 바깥패널을 띄움
    {
        if(!optionPanel.activeSelf) optionPanel.SetActive(true);
        if(!outsideOptionPanel.activeSelf) outsideOptionPanel.SetActive(true);
        //optionPanel.SetActive(!optionPanel.activeSelf);
        //outsideOptionPanel.SetActive(!outsideOptionPanel.activeSelf);
    }
    public void CloseOptionPanel()// 옵션 바깥패널을 누를 시 옵션 패널과 옵션 바깥패널을 없앰
    {
        optionPanel.SetActive (false);
        outsideOptionPanel.SetActive(false);
    }
}
