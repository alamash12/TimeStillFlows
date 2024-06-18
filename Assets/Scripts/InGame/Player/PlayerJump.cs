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
    private bool isGround = false;
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
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundCheckSize, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && playerRigid.velocity.y < 0) // 자기 자신을 제외한 충돌 감지
            {
                isGround = true;
                playerRigid.gravityScale = 2f;
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(playerRigid.velocity.y < -0.5f)
        {
            playerRigid.gravityScale = 3.5f;
        }
    }
}
