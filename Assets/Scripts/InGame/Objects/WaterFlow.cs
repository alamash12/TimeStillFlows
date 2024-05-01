using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float waterY { get; set; }
    Rigidbody2D rigid;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            StartCoroutine(Buoyancy(rigid));
        }
        if (collision.CompareTag("Player")) // 잠시 테스트로 부력 놔둠
        {
            rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;

            StartCoroutine(Buoyancy(rigid));
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
    void OnTriggerExit2D(Collider2D collision)
    {
        rigid = collision.GetComponent<Rigidbody2D>();
        if(rigid != null)
            rigid.gravityScale = 1f;
    }
}
