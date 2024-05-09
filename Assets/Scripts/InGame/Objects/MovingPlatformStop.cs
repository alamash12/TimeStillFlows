using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Player가 이동발판위에 '안착'하는 경우 아래 코드를 추가

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //레이저와 충돌하는건 나중에 구현
        if (collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D objectRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (objectRigidbody != null)
            {
                //MovingPlatform의 위치에 따라 물체의 위치 조정
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //물체가 MovingPlatform에서 떠났을 때 부모를 초기화하여 원래 상태로 되돌림
        collision.transform.SetParent(null);
    }
}
