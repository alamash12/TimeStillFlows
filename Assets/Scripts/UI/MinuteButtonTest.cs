using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinuteButtonTest : MonoBehaviour
{
    public PlayerArea playerArea;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(MinuteClicked);
    }

    void MinuteClicked()
    {
        if (playerArea.nearestObject != null)
        {
            IChangable changableComponent = playerArea.nearestObject.GetComponent<IChangable>();
            if (changableComponent != null)
            {
                playerArea.ChangeStrategy(changableComponent);
            }
        }
    }
}
