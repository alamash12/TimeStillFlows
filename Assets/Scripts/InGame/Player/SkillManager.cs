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

    [Serialize]
    public GameObject Silhouette;
    
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
            Silhouette.transform.position = playerLocation;
            Silhouette.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            Silhouette.GetComponent<SpriteRenderer>().enabled = true;
        }
        else //실루엣이 형성되어있는 상태. 플레이어를 이동시킴.
        {
            gameObject.transform.position = playerLocation;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero; // 점프하면서 이동하면 점프 가속도가 반영되던 문제 수정
            isSilhouette = false;
            Silhouette.GetComponent<SpriteRenderer>().enabled = false;

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
