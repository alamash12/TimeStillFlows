using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpPower = 5f;
    Rigidbody2D playerRigid;
    GameObject playerPivot;
    private bool isGround = false;
    private void Awake()
    {
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        playerPivot = GameObject.Find("pivot");
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
        isGround = Physics2D.Raycast(playerPivot.transform.position, Vector3.down, 0.1f); // 레이캐스트로 땅에 닿으면 점프 가능하게
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
