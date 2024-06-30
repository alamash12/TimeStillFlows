using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OptionIntro : MonoBehaviour, IPointerDownHandler
{
    Image fadeImg;
    float fadeSpeed = 0.8f;
    bool isLastScene = false;
    public void SkipIntro()
    {
        SoundManager.Instance.audioSource2.volume = PlayerPrefs.GetFloat("effectVolume");
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        SoundManager.Instance.audioSource2.volume = 0.4f;
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        int childNum = transform.childCount;

        for (int i = 0; i < childNum; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            fadeImg = child.GetComponent<Image>();

            child.SetActive(true);
            yield return StartCoroutine(FadeIn());

            yield return new WaitForSeconds(3.0f);

            if (i != childNum - 1)
            {
                yield return StartCoroutine(FadeOut());
                child.SetActive(false);
            }
            if(i == childNum - 1) isLastScene = true;
        }
    }

    IEnumerator FadeIn()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed * 2;
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        float alpha = fadeImg.color.a;
        SoundManager.Instance.EffectSoundOn("Cutscene2");

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed * 2f;
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
            yield return null;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isLastScene)
        {
            StartCoroutine(ExitSequence());
        }
    }
    IEnumerator ExitSequence()
    {
        yield return StartCoroutine(FadeOut());  // FadeOut을 완료할 때까지 기다림
        SoundManager.Instance.audioSource2.volume = PlayerPrefs.GetFloat("effectVolume");
        SceneManager.LoadScene("MainMenu");  // 페이드 아웃 완료 후 씬 로드
    }
}