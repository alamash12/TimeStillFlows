using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinuteButtonTest : MonoBehaviour
{
    MinuteArea minuteArea;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(MinuteClicked);
        minuteArea = GameObject.Find("MinuteArea").GetComponent<MinuteArea>();
    }

    public void MinuteClicked()
    {
        minuteArea.ChangeState();
    }
}
