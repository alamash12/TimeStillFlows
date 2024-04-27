using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInUnity : MonoBehaviour // ����Ƽ ������ ĳ���� �����̱� ���� ��ũ��Ʈ 
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
        if (horizontalInput < 0) // �ڷ� �̵��� �� -> �� �� ������
        {
            transform.Translate(horizontalInput * moveSpeed * Time.deltaTime * Vector3.right);
        }
        else
        {
            transform.Translate(horizontalInput * moveSpeed * Time.deltaTime * Vector3.right);
        }
    }
}

