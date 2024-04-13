using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInUnity : MonoBehaviour // 유니티 내에서 캐릭터 움직이기 위한 스크립트 
{
    Rigidbody2D rigid;

    private float moveSpeed = 5f;

    private float minX = -10f;
    private float maxX = 10f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 플레이어 이동 위치 제한 : 화면 내 // 맵 나오면 지울 항목
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        // 오른쪽 경계를 넘어간 경우 // 맵 나오면 지울 항목
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

        // 경계 내부에 있는 경우
        else
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
}

