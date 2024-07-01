using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InGameOption : MonoBehaviour
{
    public GameObject optionPanel; // 옵션 패널
    public GameObject translucentPanel; // 게임화면 50%밝기로 보이게 하기 위한 반투명 패널
    
    public void ToggleOptionPanel()// 옵션버튼을 누를 시 옵션 패널을 띄움.
    {
        if (!optionPanel.activeSelf) optionPanel.SetActive(true);
        Time.timeScale = 0.0f; // 게임을 멈춤
        translucentPanel.SetActive(true);
    }
    public void ResumeButton()// 게임을 계속해서 진행
    {
        optionPanel.SetActive(false);
        Time.timeScale = 1.0f;
        translucentPanel.SetActive(false);
    }
    public void RetryButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        SoundManager.Instance.OpeningBgmOn();
    }

    // 음량 조절 파트
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    private void Start() // 옵션창 켜지면 
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume"); // 플레이어 프렙스에 저장되어 있는 음량으로 슬라이더 설정
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.OnBgmVolumeChange); // 변경된 슬라이더값으로 오디오소스의 음량을 변경, 플레이어프렙스에 새로운 값 저장

        effectSlider.value = PlayerPrefs.GetFloat("effectVolume");
        effectSlider.onValueChanged.AddListener(SoundManager.Instance.OnEffectVolumeChange);
    }
}
