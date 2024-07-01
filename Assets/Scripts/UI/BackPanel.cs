using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackPanel : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject htpPanel;
    [SerializeField] GameObject madeByPanel;
    public void OnPointerDown(PointerEventData eventData)
    {
        if ((!(htpPanel.activeSelf) && !(madeByPanel.activeSelf)))
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

}
