using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField] private float moveSpeed = 5f;
    GameObject player;

    Vector2 lastPosition;
    float deltaX; // 드래그의 강도
    float direction = 0;
    bool isDragging = false;
    Action moveFunc;

    float previousX; // 효과음 삽입을 위한 변수 설정
    bool isMoving = false; // 효과음을 위한 변수
    Rigidbody2D playerRigid;

    Animator animator;

    void Awake()
    {
        player = GameObject.Find("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        previousX = gameObject.transform.position.x;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        lastPosition = eventData.position;
        isDragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        moveFunc = null;
        isDragging = false;
        animator.SetBool("isWalk", false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isDragging = false;
        animator.SetBool("isWalk", false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        deltaX = eventData.position.x - lastPosition.x;
        moveFunc -= Move;
        moveFunc += Move;
        lastPosition = eventData.position;
    }
    private void Move() 
    {
        if (Mathf.Abs(deltaX) > 3f) // 감도 조절을 위해서 
            direction = Mathf.Sign(deltaX);

        if (direction < 0) // 드래그하는 방향에 따라서 방향 애니메이터 변수 조절
            animator.SetBool("isRight", false);
        else
            animator.SetBool("isRight", true);

        animator.SetBool("isWalk", true);
        player.transform.Translate(direction * moveSpeed * Time.deltaTime * Vector3.right);

        if (!isMoving && playerRigid.velocity.y == 0)
        {
            isMoving = true;
            StartCoroutine(SFXPlay());
        }
    }
    private void Update()
    {
        if (!isDragging && isMoving)
        {
            isMoving = false;
            StopCoroutine(SFXPlay());
            SoundManager.Instance.EffectSoundOff();
        }
    }
    void FixedUpdate()
    {
        if (isDragging)
        {
            moveFunc?.Invoke();
        }
    }

    IEnumerator SFXPlay()
    {
        while(true)
        {
            while (isMoving && playerRigid.velocity.y == 0)
            {
                SoundManager.Instance.EffectSoundOn("Walk");
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }
}
