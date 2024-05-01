using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStop : MonoBehaviour
{
    public float waterY { get; set; }
    Rigidbody2D rigid;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
        }
        if (collision.CompareTag("Player")) // ��� �׽�Ʈ�� �η� ����
        {
            rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            //���� ���� ����
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        rigid = collision.GetComponent<Rigidbody2D>();
        if (rigid != null)
            rigid.gravityScale = 1f;
    }
}
