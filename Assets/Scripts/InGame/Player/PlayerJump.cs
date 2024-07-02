using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpPower;
    [SerializeField] Transform groundCheck;    

    public Rigidbody2D playerRigid;
    public bool isGround = false;
    Animator animator;
    private void Awake()
    {
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    public void Jump()
    {
        Debug.Log(isGround);
        if (isGround && playerRigid.velocity.y >= 0)
        {
            playerRigid.velocity = new Vector2 (0, 0);
            SoundManager.Instance.EffectSoundOn("Jump");
            isGround = false;
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, 0);
            playerRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(!isGround && playerRigid.velocity.y < -0.5f)
        {
            playerRigid.gravityScale = 3.5f;
        }
    }
    
    public void JumpStateReset()
    {
        playerRigid.gravityScale = 2f;
        isGround = true;
        animator.SetBool("isJump", false);
    }
}
