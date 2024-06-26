using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Button minuteButton;
    public Button hourButton;
    public Button gearButton;
    public HourArea hourArea;
    MinuteArea minuteArea;
    
    float cooltime = 1f;
    bool isCooltime = false;
    bool isSilhouette = false;
    Vector3 playerLocation;
    void Awake()
    {
        minuteArea = gameObject.transform.GetChild(0).GetComponent<MinuteArea>();
    }

    void Start()
    {
        minuteButton.onClick.AddListener(MinuteClicked);
        hourButton.onClick.AddListener (HourClicked);
        gearButton.onClick.AddListener(GearClicked);
    }

    public void HourClicked()
    {
        hourArea.ChangeState();
        if(!isCooltime)
        {
            StartCoroutine(ApplyCooldown());
        }
    }
    public void MinuteClicked()
    {
        minuteArea.ChangeState();
        if (!isCooltime)
        {
            StartCoroutine(ApplyCooldown());
        }
    }
    public void GearClicked()
    {
        if (!isSilhouette)
        {
            playerLocation = gameObject.transform.position;
            isSilhouette = true;
        }
        else //실루엣이 형성되어있는 상태. 플레이어를 이동시킴.
        {
            gameObject.transform.position = playerLocation;
            isSilhouette = false;
        }

        if (!isCooltime)
        {
            StartCoroutine(ApplyCooldown());
        }
    }
    IEnumerator ApplyCooldown()
    {
        isCooltime = true;
        minuteButton.interactable = false;
        hourButton.interactable = false;
        gearButton.interactable = false;

        yield return new WaitForSeconds(cooltime);

        minuteButton.interactable = true;
        hourButton.interactable = true;
        gearButton.interactable = true;
        isCooltime = false;
        yield break;
    }
}
