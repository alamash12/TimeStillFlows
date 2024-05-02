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
        minuteArea = GameObject.Find("minuteArea").GetComponent<MinuteArea>();
    }

    public void MinuteClicked()
    {
        if (minuteArea.nearestObject != null)
        {
            IChangable changableComponent = minuteArea.nearestObject.GetComponent<IChangable>();
            if (changableComponent != null)
            {
                minuteArea.ChangeStrategy(changableComponent);
            }
        }
    }
}
