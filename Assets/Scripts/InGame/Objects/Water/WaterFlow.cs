using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    private void Awake()
    {
        foreach(GameObject gameObject in gameObject.GetComponent<Water>().TriggeredBlock) // 상태가 변경되었을때 물에 있는 블록을 레이캐스트 불가능하게 변경
        {
            gameObject.layer = 2;
        }
    }
}
