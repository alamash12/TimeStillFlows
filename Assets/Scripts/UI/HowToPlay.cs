using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 스테이지 1에 뜨는 가이드에 관하여
 */
public class HowToPlay : MonoBehaviour
{
    [SerializeField] GameObject htpPanel;
    [SerializeField] GameObject backPanel;
    [SerializeField] GameObject htp2Panel;
    private void Start()
    {
        Time.timeScale = 0.0f;
    }
    public void ClosePanel()
    {
        Time.timeScale = 1.0f;
        htpPanel.SetActive(false);
        htp2Panel.SetActive(false);
        backPanel.SetActive(false);
    }
    public void NextBtn()
    {
        htp2Panel.SetActive(true);
    }
    public void PrevBtn()
    {
        htp2Panel.SetActive(false);
    }
}
