using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStop : MonoBehaviour, IWaterStrategy
{
    float waterY;
    new Rigidbody2D rigidbody2D = new Rigidbody2D();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {
            rigidbody2D = collision.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0f;
        }
        if (collision.tag == "Player") // ��� �׽�Ʈ�� �η� ����
        {
            rigidbody2D = collision.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0f;
            //���� ���� ����
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        rigidbody2D = collision.GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 1f;
    }
    public void SetWaterY(float waterY)
    {
        this.waterY = waterY;
    }
}
