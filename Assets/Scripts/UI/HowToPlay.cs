using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject backPanel;
    private void Start()
    {
        Time.timeScale = 0.0f;
    }
    public void ClosePanel()
    {
        Time.timeScale = 1.0f;
        panel.SetActive(false);
        backPanel.SetActive(false);
    }
}
