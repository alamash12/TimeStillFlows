using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    float waterY;
    Dictionary<Rigidbody2D, Coroutine> activeCoroutines = new Dictionary<Rigidbody2D, Coroutine>(); // 코루틴을 저장하는 용도
    public List<GameObject> TriggedBlock = new();
    private void Awake()
    {
        waterY = gameObject.GetComponent<Water>().waterY;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            TriggedBlock.Add(rigid.gameObject);
            rigid.gameObject.layer = 2;
            activeCoroutines.Add(rigid, StartCoroutine(Buoyancy(rigid)));
        }
        if (collision.CompareTag("Player")) // 잠시 테스트로 부력 놔둠
        {
            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            activeCoroutines.Add(rigid, StartCoroutine(Buoyancy(rigid)));
            //게임 오버 구현
        }
    }

    IEnumerator Buoyancy(Rigidbody2D collision)
    {
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
            TriggedBlock.Remove(rigid.gameObject);
        }
    }
}
