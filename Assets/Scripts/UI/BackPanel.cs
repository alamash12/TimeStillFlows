using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// backPanel을 누르면 팝업을 닫게 하는 기능을 담은 스크립트
public class BackPanel : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject htpPanel;
    [SerializeField] GameObject madeByPanel;
    [SerializeField] GameObject warningPanel;
    public void OnPointerDown(PointerEventData eventData) // backPanel을 눌렀을 시에
    {
        // howtoplay 패널과 madeby 패널이 켜져있다면 바깥은 눌러도 꺼지지 않고
        // 두 패널이 꺼져있다면 optionPanel만 켜져있으므로 바깥을 누르면 꺼지도록 함.
        if ((!(htpPanel.activeSelf) && !(madeByPanel.activeSelf) && !(warningPanel.activeSelf)))
        {
            // 꺼져있으면 키고 켜져있으면 끈다.. if문에 backPanel이 실행되는 모든 panel을 집어넣지 않으면 클릭시 설정창이 켜짐.
            optionPanel.SetActive(false); 
            gameObject.SetActive(false);
        }
    }
}
