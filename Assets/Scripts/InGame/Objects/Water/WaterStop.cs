using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStop : MonoBehaviour
{
    private void Awake()
    {
        foreach (GameObject gameObject in gameObject.GetComponent<Water>().TriggeredBlock) // 상태가 변경되었을때 물에 닿아있는 블록을 레이캐스트 가능하게 변경
        {
            gameObject.layer = 0;
        }
    }
}
