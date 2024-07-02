using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * "메인화면"에서 뜨는 가이드에 관하여
 */
public class HowToPlayMain : MonoBehaviour
{
    [SerializeField] GameObject HtpPanel;
    [SerializeField] GameObject Htp2Panel;
    public void OpenHtpPanel() // 가이드 버튼에 붙음
    {
        HtpPanel.SetActive(true);
    }
    public void CloseHtpPanel() // x 버튼에 붙음
    {
        HtpPanel.SetActive(false);
        Htp2Panel.SetActive(false);
    }
    public void NextBtn()
    {
        Htp2Panel.SetActive(true);
    }
    public void PrevBtn()
    {
        Htp2Panel.SetActive(false);
    }
}
