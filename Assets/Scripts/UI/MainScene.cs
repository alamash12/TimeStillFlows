using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene: MonoBehaviour
{
    /*
     * MainScene : 메인화면에서 일어나는 씬의 전환과 각종 상호작용들을 저장하는 클래스입니다.
     */
    
    public void StartNewGame() // NewGame을 누를 시 Intro화면으로 씬을 전환함.
    {
        PlayerPrefs.DeleteKey("Stage02");
        PlayerPrefs.DeleteKey("Stage03");
        PlayerPrefs.DeleteKey("Stage04");
        PlayerPrefs.DeleteKey("Stage05");
        PlayerPrefs.DeleteKey("Stage06");
        PlayerPrefs.DeleteKey("Stage07");
        PlayerPrefs.DeleteKey("Stage08");

        PlayerPrefs.SetInt("Stage01", 1);
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
    
    public void ToggleOptionPanel()// 옵션버튼을 누를 시 옵션 패널 생성
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
        //optionPanel.SetActive(true);
    }
    public void CloseOptionPanel()// 옵션 x 버튼을 누를 시 옵션 패널 없앰
    {
        optionPanel.SetActive (false);
    }
    public void OptionIntroBtn()
    {
        SceneManager.LoadScene("OptionIntro");
    }
    
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("bgmVolume"));
        //bgmSlider.value = SoundManager.Instance.bgmVolume; // SoundManager의 bgmVolume을 슬라이더 값에 넣는다.
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.OnBgmVolumeChange);

        //effectSlider.value = SoundManager.Instance.effectVolume;
        effectSlider.value = PlayerPrefs.GetFloat("effectVolume");
        effectSlider.onValueChanged.AddListener(SoundManager.Instance.OnEffectVolumeChange);
    
    }
}
