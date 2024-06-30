using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpPower;
    [SerializeField] Transform groundCheck;    
    Vector2 groundCheckSize = new Vector2(1f, 0.05f); // 점프를 위한 OverlapBox 크기변수
    Rigidbody2D playerRigid;
    bool isGround = false;
    bool isJumping = false;
    Animator animator;
    private void Awake()
    {
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    public void Jump()
    {
        if (isGround && !isJumping)
        {
            isGround = false;
            isJumping = true;
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, 0);
            playerRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
            Invoke("ResetJumping", 0.1f);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(playerRigid.velocity.y < -0.5f)
        {
            playerRigid.gravityScale = 3.5f;
        }
    }
    private void LateUpdate()
    {
        Collider2D collider = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f);
        if (collider != null && !isJumping)
        {
            if (collider.gameObject != gameObject) // 자기 자신을 제외한 충돌 감지
            {
                JumpStateReset();
            }
        }
    }
    public void JumpStateReset()
    {
        playerRigid.gravityScale = 2f; // 스케일 조절로 인해서 lateUpdate 사용
        isGround = true;
        animator.SetBool("isJump", false);
    }

    void ResetJumping()
    {
        isJumping = false; // 일정 시간 후에 다시 점프할 수 있도록 설정
    }
}
