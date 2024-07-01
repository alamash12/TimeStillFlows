using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InGameOption : MonoBehaviour
{
    public GameObject optionPanel; // 옵션 패널
    public GameObject translucentPanel; // 게임화면 50%밝기로 보이게 하기 위한 반투명 패널
    //public GameObject outsideOptionPanel; // 옵션 바깥패널

    public void ToggleOptionPanel()// 옵션버튼을 누를 시 옵션 패널을 띄움.
    {
        if (!optionPanel.activeSelf) optionPanel.SetActive(true);
        //if (!outsideOptionPanel.activeSelf) outsideOptionPanel.SetActive(true);
        Time.timeScale = 0.0f; // 게임을 멈춤
        translucentPanel.SetActive(true);
    }
    public void ResumeButton()// 게임을 계속해서 진행
    {
        optionPanel.SetActive(false);
        //outsideOptionPanel.SetActive(false);
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
    private void Start()
    {
        //bgmSlider.value = SoundManager.Instance.bgmVolume; // SoundManager의 bgmVolume을 슬라이더 값에 넣는다.
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.OnBgmVolumeChange);

        //effectSlider.value = SoundManager.Instance.effectVolume;
        effectSlider.value = PlayerPrefs.GetFloat("effectVolume");
        effectSlider.onValueChanged.AddListener(SoundManager.Instance.OnEffectVolumeChange);
    }
}
