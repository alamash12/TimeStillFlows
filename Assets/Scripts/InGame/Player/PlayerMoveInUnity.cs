using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInUnity : MonoBehaviour // 유니티 내에서 캐릭터 움직이기 위한 스크립트 
{
    Rigidbody2D rigid;

    private float moveSpeed = 5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0) // 뒤로 이동할 때 -> 좀 더 빠르게
        {
            transform.Translate(horizontalInput * moveSpeed * Time.deltaTime * Vector3.right);
        }
        else
        {
            transform.Translate(horizontalInput * moveSpeed * Time.deltaTime * Vector3.right);
        }
    }
}

