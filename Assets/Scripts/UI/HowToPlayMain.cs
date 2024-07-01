using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayMain : MonoBehaviour
{
    [SerializeField] GameObject HtpPanel;
    public void OpenHtpPanel()
    {
        HtpPanel.SetActive(true);
    }
    public void CloseHtpPanel()
    {
        HtpPanel.SetActive(false);
    }
}
