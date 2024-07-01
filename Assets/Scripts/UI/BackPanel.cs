using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackPanel : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] GameObject optionPanel;
    public void OnPointerDown(PointerEventData eventData)
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
