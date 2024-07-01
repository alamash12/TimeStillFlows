using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Intro : MonoBehaviour, IPointerDownHandler
{
    Image fadeImg; // 페이드 효과를 줄 현재 이미지
    float fadeSpeed = 0.8f; // 클수록 빠르게 페이드 인, 아웃
    bool isLastScene = false;
    public void SkipIntro()
    {
        SceneManager.LoadScene("Stage_01");
    }

    private void Start()
    {
        StartCoroutine(SceneChange());
    }

    // 모든 씬들을 담고 있는 Scenes 오브젝트에 붙어, 자식들을 하나씩 확인하며 동작
    IEnumerator SceneChange()
    {
        int childNum = transform.childCount;

        for (int i = 0; i < childNum; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            fadeImg = child.GetComponent<Image>();

            child.SetActive(true); // 해당 컷씬을 활성화
            yield return StartCoroutine(FadeIn()); // 페이드 인, 서서히 나타나게 함.

            yield return new WaitForSeconds(3.0f); // 3초동안 정지

            if (i != childNum - 1)
            {
                yield return StartCoroutine(FadeOut()); // 페이드 아웃, 서서히 사라짐.
                child.SetActive(false); // 해당 컷씬을 비활성화
            }
            if(i == childNum - 1) isLastScene = true; // 마지막 씬이라면 
        }
    }

    IEnumerator FadeIn()
    {
        float alpha = 0;
        // 서서히 투명도를 올린다.
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed * 2;
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        SoundManager.Instance.EffectSoundOn("Cutscenen"); // 컷씬 넘어갈 때 소리

        float alpha = fadeImg.color.a;
        // 서서히 투명도를 내린다.
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed * 2f;
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, alpha);
            yield return null;
        }
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
        // 만약 라스트 씬이라면 화면 클릭 시 페이드 아웃 후 스테이지1로
        if (isLastScene)
        {
            StartCoroutine(ExitSequence());
        }
    }
    IEnumerator ExitSequence()
    {
        yield return StartCoroutine(FadeOut());  // FadeOut을 완료할 때까지 기다림
        SceneManager.LoadScene("Stage_01");  // 페이드 아웃 완료 후 씬 로드
    }
}