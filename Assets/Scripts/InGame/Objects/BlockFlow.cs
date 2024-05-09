using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFlow : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //레이저와 충돌하는건 나중에 구현
        if (collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D objectRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            //MovingPlatform은 Block을 Parent로 두지 않아야 함
            //따라서 자신보다 위치가 높을 때만 Parent 설정 
            if (objectRigidbody != null && objectRigidbody.transform.position.y > transform.position.y)
            {
                //MovingPlatform의 위치에 따라 물체의 위치 조정
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    { 
        //물체가 Block을 벗어났을 때 부모를 초기화하여 원래 상태로 되돌림
        if (collision.transform.parent == gameObject)
        {
            collision.transform.SetParent(null);
        }
    }
}
