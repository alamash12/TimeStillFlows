using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

      private void OnCollisionEnter2D(Collision2D collision)
    {
        // MovingPlatform과 서로 Parant인 겨우 우선권 양보 
        if (collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D objectRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (objectRigidbody != null && objectRigidbody.transform.position.y > transform.position.y)
            {
                //MovingPlatform의 위치에 따라 물체의 위치 조정
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //물체가 MovingPlatform에서 떠났을 때 부모를 초기화하여 원래 상태로 되돌림
        if(collision.transform.parent == gameObject)
        {
             collision.transform.SetParent(null);
        }
      
    }
}
