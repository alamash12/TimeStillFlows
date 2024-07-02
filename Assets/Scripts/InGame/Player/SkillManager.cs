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
        minuteButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f; // 이미지가 있는 부분만 클릭하도록 설정
        hourButton.onClick.AddListener (HourClicked);
        hourButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        gearButton.onClick.AddListener(GearClicked);
        gearButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
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
            gameObject.GetComponent<PlayerJump>().isGround = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero; // 점프하면서 돌아오면 점프 가속도가 반영되던 문제 수정
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
        SoundManager.Instance.EffectSoundOn("SkillSound");
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
