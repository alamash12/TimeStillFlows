using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class WaterFlow : MonoBehaviour , IWaterStrategy
{
    public float waterY;
    new Rigidbody2D rigidbody2D = new Rigidbody2D();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {
            rigidbody2D = collision.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0f;
            StartCoroutine(Buoyancy(rigidbody2D));
        }
        if (collision.tag == "Player") // 잠시 테스트로 부력 놔둠
        {
            rigidbody2D = collision.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0f;

            StartCoroutine(Buoyancy(rigidbody2D));
            //게임 오버 구현
        }
    }

    IEnumerator Buoyancy(Rigidbody2D collision)
    {
        yield return new WaitForSeconds(0.15f);
        while(collision.transform.position.y < waterY)
        {
            collision.velocity = new Vector2(0, Mathf.Clamp((collision.velocity.y + 0.07f), -10, 5));
            yield return null;
        }
        collision.velocity = new Vector2(0, 0);
        yield break;
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
