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
}
