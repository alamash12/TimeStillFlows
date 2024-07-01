using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] GameObject panel;
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
