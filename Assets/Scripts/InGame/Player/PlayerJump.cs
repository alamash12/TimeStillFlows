using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpPower = 5f;
    Rigidbody2D playerRigid;
    private bool isGround = false;
    private void Awake()
    {
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
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
        isGround = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 0.6f); // 레이캐스트로 땅에 닿으면 점프 가능하게
        Debug.DrawRay(gameObject.transform.position, Vector2.down * 0.6f, Color.red); // 나중에 캐릭터 크기에 따라서 조절
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
