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

    [SerializeField] AudioClip bgmWood;
    [SerializeField] AudioClip bgmTown;
    [SerializeField] AudioClip bgmOpening;
    private AudioSource audioSource1; // 배경음
    public AudioSource audioSource2; // 효과음
    float stageNum; // 스테이지의 번호
    void Start() // 게임 처음 시작시 음악세팅
    {
        if (!PlayerPrefs.HasKey("bgmVolume")) PlayerPrefs.SetFloat("bgmVolume", 1.0f);
        if (!PlayerPrefs.HasKey("effectVolume")) PlayerPrefs.SetFloat("effectVolume", 1.0f);

        audioSource1 = gameObject.AddComponent<AudioSource>(); // audioSource에 AudioSource 컴포넌트를 추가
        audioSource1.loop = true;
        audioSource2 = gameObject.AddComponent<AudioSource>();

        if (bgmWood == null) // bgmWood AudioClip에 클립 추가
        {
            bgmWood = Resources.Load<AudioClip>("Audio/Background/WoodFinal");
        }
        if (bgmTown == null) // bgmTown AudioClip에 클립 추가
        {
            bgmTown = Resources.Load<AudioClip>("Audio/Background/TownFinal");
        }
        if (bgmOpening == null)
        {
            bgmOpening = Resources.Load<AudioClip>("Audio/Background/Opening");
        }
        audioSource1.clip = bgmOpening; // 메인화면에서 재생할 클립 bgmWood
        audioSource1.Play(); // 재생
        
    }
    /*void Update() // 씬 바뀌면 어떤 bgm을 틀것인가?
    {
        stageNum = SceneManager.GetActiveScene().buildIndex;
        if (audioSource1.clip != bgmOpening && (stageNum == 0))
        {
            audioSource1.clip = bgmOpening;
            audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume");
            audioSource1.Play();
        }
        if (audioSource1.clip != bgmWood && stageNum >= 4 && stageNum <= 7)
        {
            audioSource1.clip = bgmWood;
            audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume");
            audioSource1.Play();
        }
        
        if (audioSource1.clip != bgmTown && stageNum >= 8 && stageNum >= 11)
        {
            audioSource1.clip = bgmTown;
            audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume");
            audioSource1.Play();
        }


    }*/
    public void OpeningBgmOn()
    {
        audioSource1.clip = bgmOpening;
        audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume");
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
    /*string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }*/
    //public float bgmVolume = 1.0f;// 게임 내에서 공유하는 bgm 슬라이드 값
    //public float effectVolume = 1.0f; // 게임 내에서 공유하는 효과음 슬라이드 값

    public void OnBgmVolumeChange(float volume)
    {
        audioSource1.volume = volume;
        //bgmVolume = volume;
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }
    public void OnEffectVolumeChange(float volume)
    {
        audioSource2.volume = volume;
        //effectVolume = volume;
        PlayerPrefs.SetFloat("effectVolume", volume);
    }

    public void EffectSoundOn(string effectName) // 원하는 곳에 효과음 추가 함수
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
