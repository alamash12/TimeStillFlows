using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFlow : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;       
    }
}
