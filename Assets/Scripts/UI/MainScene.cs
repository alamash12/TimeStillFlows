using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene: MonoBehaviour
{
    /*
     * MainScene : 메인화면에서 일어나는 씬의 전환과 각종 상호작용들을 저장하는 클래스입니다.
     */
    [SerializeField] GameObject warningPanel;
    [SerializeField] GameObject backPanel;
    public void NewGame() // NewGame을 누를 시 Intro화면으로 씬을 전환함.
    {
        // 1스테이지를 클리어했다면 경고창을 띄우고 게임시작
        if(PlayerPrefs.GetInt("Stage_02") == 1) 
        {
            warningPanel.SetActive(true);
            backPanel.SetActive(true);
        }// 처음이라면 띄우지 않음
        else if(PlayerPrefs.GetInt("Stage_02") == 0) 
        {
            StartNewGame();
        }
        
    }
    public void StartNewGame()
    {
        // 새로운 게임 시작시 기존 PlayerPrefs 데이터 삭제
        PlayerPrefs.DeleteKey("Stage_02");
        PlayerPrefs.DeleteKey("Stage_03");
        PlayerPrefs.DeleteKey("Stage_04");
        PlayerPrefs.DeleteKey("Stage_05");
        PlayerPrefs.DeleteKey("Stage_06");
        PlayerPrefs.DeleteKey("Stage_07");
        PlayerPrefs.DeleteKey("Stage_08");

        PlayerPrefs.SetInt("Stage_01", 1); // 1스테이지 시작 시 PlayerPrefs에 1 저장
        SceneManager.LoadScene("Intro");

        SoundManager.Instance.WoodBgmOn();
    }
    //// Warning 패널 관련/////
    public void yesBtn()
    {
       StartNewGame();
    }
    public void noBtn()
    {
        warningPanel.SetActive(false);
        backPanel.SetActive(false);
    }
    //////////
    public void ContinueGame() 
    {
        SceneManager.LoadScene("StageSelect"); 
    }
    public void GameQuit()
    {
        Application.Quit();
    }

    /*
       옵션 패널관련
    */
    [SerializeField] GameObject optionPanel; // 옵션 패널 
    public void ToggleOptionPanel()// 옵션버튼을 누를 시 optionPanel과 backPanel 생성
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
        backPanel.SetActive(!backPanel.activeSelf);
    }
    public void CloseOptionPanel()// 옵션 x 버튼을 누를 시 optionPanel,backPanel 없앰
    {
        optionPanel.SetActive (false);
        backPanel.SetActive (false);
    }
    public void OptionIntroBtn()
    {
        SceneManager.LoadScene("OptionIntro");
    }
    public void OptionEndingBtn()
    {
        if(PlayerPrefs.GetInt("Ending") == 1) // 엔딩까지 클리어 했을 시 씬 전환
        {
            SceneManager.LoadScene("Ending");
        }
    }
    
    // 음량 조절 파트
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    private void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume"); // PlayerPrefs에 저장되어 있는 음량으로 슬라이더 설정
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.OnBgmVolumeChange);// 변경된 슬라이더값으로 오디오소스의 음량을 변경, PlayerPrefs에 새로운 값 저장

        effectSlider.value = PlayerPrefs.GetFloat("effectVolume");
        effectSlider.onValueChanged.AddListener(SoundManager.Instance.OnEffectVolumeChange);
    }
}
