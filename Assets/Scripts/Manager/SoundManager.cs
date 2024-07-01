using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    AudioClip bgmWood; // 숲 배경음악 저장 오디오클립
    AudioClip bgmTown; // 마을 배경음악 저장 오디오클립
    AudioClip bgmOpening; // 메인화면 배경음악 저장 오디오클립

    private AudioSource audioSource1; // 배경음 오디오소스, 배경음들을 저장해서 사용함
    private AudioSource audioSource2; // 효과음 오디오소스, 효과음들을 저장해서 사용함
    
    void Start() // 게임 처음 시작시 음악세팅
    {
        // 만약 플레이어 프렙스에 저장된 bgm과 effect의 Volume값이 있다면 불러온다. 게임이 꺼졌다 켜져도 전의 값을 유지하기 위함.
        if (!PlayerPrefs.HasKey("bgmVolume")) PlayerPrefs.SetFloat("bgmVolume", 1.0f); 
        if (!PlayerPrefs.HasKey("effectVolume")) PlayerPrefs.SetFloat("effectVolume", 1.0f);

        // audioSource에 AudioSource 컴포넌트를 추가
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource1.loop = true; 
        
        // 오디오 클립에 오디오 추가
        bgmWood = Resources.Load<AudioClip>("Audio/Background/WoodFinal");
        bgmTown = Resources.Load<AudioClip>("Audio/Background/TownFinal");
        bgmOpening = Resources.Load<AudioClip>("Audio/Background/Opening");

        OpeningBgmOn(); // 게임 시작시 메인메뉴에서 오프닝Bgm 재생
    }
    
    public void OpeningBgmOn() 
    {
        audioSource1.clip = bgmOpening;
        audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume"); // 플레이어프렙스에서 bgmVolume 값 가져오기
        audioSource1.Play();
    }
    public void WoodBgmOn()
    {
        audioSource1.clip = bgmWood;
        audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume");
        audioSource1.Play();
    }
    public void TownBgmOn()
    {
        audioSource1.clip = bgmTown;
        audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume");
        audioSource1.Play();
    }

    //옵션창 음향 슬라이더에서 값 변경시 오디오소스의 볼륨을 조절하고 이 값을 플레이어 프렙스에 저장
    public void OnBgmVolumeChange(float volume)
    {
        audioSource1.volume = volume;
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }
    public void OnEffectVolumeChange(float volume)
    {
        audioSource2.volume = volume;
        PlayerPrefs.SetFloat("effectVolume", volume);
    }

    // 원하는 곳에 효과음 추가 위한 함수
    // SoundManager.Instance.EffectSoundOn("Walk")와 같이 사용
    public void EffectSoundOn(string effectName) 
    {
        string effect = "Audio/Effect/" + effectName;
        AudioClip effectClip = Resources.Load<AudioClip>(effect);
        audioSource2.clip = effectClip;
        audioSource2.PlayOneShot(effectClip);
    }

    public void EffectSoundOff()
    {
        audioSource2.Stop();
    }
}
