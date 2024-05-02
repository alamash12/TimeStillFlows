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
        if(hourArea.triggeredObject != null)
        {
            foreach (GameObject gameObject in hourArea.triggeredObject)
            {
                IChangable changable = gameObject.GetComponent<IChangable>();
                if(changable != null)
                {
                    hourArea.ChangeStrategy(changable);
                }
            }
        }
    }
}
