using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = new Vector2(0,0); // 가속도 초기화
    }
}
