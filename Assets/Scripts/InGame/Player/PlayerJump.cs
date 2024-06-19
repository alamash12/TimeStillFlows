using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpPower;
    Rigidbody2D playerRigid;
    [SerializeField] GameObject pivot;
    [SerializeField] Transform groundCheck;
    Vector2 groundCheckSize = new Vector2(1f, 0.05f);
    public bool isGround = false;
    Animator animator;
    private void Awake()
    {
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    public void Jump()
    {
        if (isGround)
        {
            isGround = false;
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, 0);
            playerRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            animator.SetBool("isJump", true);
        }
        if(playerRigid.velocity.y < -0.5f)
        {
            playerRigid.gravityScale = 3.5f;
        }
    }
    private void LateUpdate()
    {
        Collider2D collider = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f);
        if (collider != null)
        {
            if (collider.gameObject != gameObject && playerRigid.velocity.y < 0) // 자기 자신을 제외한 충돌 감지
            {
                playerRigid.gravityScale = 2f; // 스케일 조절로 인해서 lateUpdate 사용
                isGround = true;
                animator.SetBool("isJump", false);
            }
        }
    }
}
