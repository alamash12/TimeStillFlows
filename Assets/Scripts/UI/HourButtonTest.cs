using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HourButtonTest : MonoBehaviour
{
    HourArea hourArea;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HourClicked);
        hourArea = GameObject.Find("HourArea").GetComponent<HourArea>();
    }

    public void HourClicked()
    {
        hourArea.ChangeState();
    }
}
