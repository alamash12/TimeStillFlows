using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField] private float moveSpeed = 5f;
    GameObject player;

    private Vector2 lastPosition;
    private float deltaX;
    private float direction = 0;
    private bool isDragging = false;
    Action moveFunc;

    Animator animator;

    void Awake()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
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
        if (Mathf.Abs(deltaX) > 1) // 감도 조절을 위해서 
            direction = Mathf.Sign(deltaX);

        if (direction < 0) // 드래그하는 방향에 따라서 방향 애니메이터 변수 조절
            animator.SetBool("isRight", false);
        else
            animator.SetBool("isRight", true);

        animator.SetBool("isWalk", true);
        player.transform.Translate(direction * moveSpeed * Time.deltaTime * Vector3.right);
    }
    void FixedUpdate()
    {
        if (isDragging)
        {
            moveFunc?.Invoke();
        }
    }
}
