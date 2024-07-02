using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerJump playerJump;

    private void Start()
    {
        playerJump = GetComponentInParent<PlayerJump>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player") && !collision.CompareTag("Area") && playerJump.playerRigid.velocity.y < 0)
        {
            playerJump.JumpStateReset();
            Debug.Log(collision.name);
        }
    }
}
