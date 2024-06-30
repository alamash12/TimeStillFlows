using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour, IPointerDownHandler
{
    Image fadeImg;
    float fadeSpeed = 0.8f; // 작을수록 빠르게 페이드 인, 아웃
    bool isLastScene = false;
    public void SkipEnding()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        int childNum = transform.childCount;
        float waitSec = 3.0f; // 씬 전환되기 전에 멈춰있는 시간

        for (int i = 0; i < childNum; i++)
        {
            if (i == 7) waitSec = 4.0f; 

            GameObject child = transform.GetChild(i).gameObject;
            fadeImg = child.GetComponent<Image>();

            child.SetActive(true);
            yield return StartCoroutine(FadeIn());

            yield return new WaitForSeconds(waitSec);

            if (i < 7)
            {
                yield return StartCoroutine(FadeOut());
                child.SetActive(false);
            }
            if(i == 7 || i == 8)
            {
                fadeSpeed = 0.3f;
                yield return StartCoroutine(FadeOut());
                child.SetActive(false);
            }
            if (i == childNum - 1) isLastScene = true;
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
        SoundManager.Instance.EffectSoundOn("Cutscenen");

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
            Image backGround = GameObject.Find("Background").GetComponent<Image>();
            backGround.color = Color.black;
            StartCoroutine(ExitSequence());
        }
    }
    IEnumerator ExitSequence()
    {
        yield return StartCoroutine(FadeOut());  // FadeOut을 완료할 때까지 기다림
        SceneManager.LoadScene("MainMenu");  // 페이드 아웃 완료 후 씬 로드
    }
}
