using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStop : MonoBehaviour
{
    float waterY;
    Rigidbody2D rigid;

    private void Awake()
    {
        waterY = gameObject.GetComponent<Water>().waterY;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
        }
        if (collision.CompareTag("Player")) // 잠시 테스트로 부력 놔둠
        {
            rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            //게임 오버 구현
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        rigid = collision.GetComponent<Rigidbody2D>();
        if (rigid != null)
            rigid.gravityScale = 1f;
    }
}
