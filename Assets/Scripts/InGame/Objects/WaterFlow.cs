using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float waterY { get; set; }
    private Dictionary<Rigidbody2D, Coroutine> activeCoroutines = new Dictionary<Rigidbody2D, Coroutine>(); // 코루틴을 저장하는 용도

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            Coroutine coroutine = StartCoroutine(Buoyancy(rigid));
            activeCoroutines.Add(rigid, coroutine);
        }
        if (collision.CompareTag("Player")) // 잠시 테스트로 부력 놔둠
        {
            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            Coroutine coroutine = StartCoroutine(Buoyancy(rigid));
            activeCoroutines.Add(rigid, coroutine);
            //게임 오버 구현
        }
    }

    IEnumerator Buoyancy(Rigidbody2D collision)
    {
        Debug.Log(collision.name);
        while(true)
        {
            yield return new WaitForSeconds(0.15f);
            while (collision.transform.position.y < waterY)
            {
                collision.velocity = new Vector2(0, Mathf.Clamp((collision.velocity.y + 0.07f), -10, 5));
                yield return null;
            }
            collision.velocity = new Vector2(0, 0);
            yield return new WaitWhile(() => collision.velocity.y >= 0); // 물체가 아래로 떨어지기 전까지 대기상태
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
        if (rigid != null && activeCoroutines.ContainsKey(rigid))
        {
            StopCoroutine(activeCoroutines[rigid]);
            activeCoroutines.Remove(rigid);
            rigid.gravityScale = 1f;
        }
    }
}
