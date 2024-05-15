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
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if(bgmWood == null) 
        {
            bgmWood = Resources.Load<AudioClip>("Audio/Background/Wood");
        }
        if(bgmTown == null)
        {
            bgmTown = Resources.Load<AudioClip>("Audio/Background/Town");
        }
        audioSource.clip = bgmWood;
        audioSource.Play();
    }
    void Update()
    {
        if( audioSource.clip != bgmWood && SceneManager.GetActiveScene().name == "MainMenu")
        {
            audioSource.clip = bgmWood;
            audioSource.Play();
        }
        if (audioSource.clip != bgmTown && SceneManager.GetActiveScene().name == "Stage01")
        {
            audioSource.clip = bgmTown;
            audioSource.Play();
        }
    }

    public float bgmVolume = 1.0f;// 메인화면과 인게임 옵션 bgm 슬라이드 값을 이 값으로 통일한다.
    public float effentVolume = 1.0f; //
    public void OnVolumeChange(float volume)
    {
        audioSource.volume = volume;
        bgmVolume = volume;
    }

}
