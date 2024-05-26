using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformFlow : MonoBehaviour
{
    public float speed = 2f; //이동 속도
    public Vector3 startLocation; // 시작 위치
    public Vector3 endLocation; //종료 위치 

    private Vector3 startPosition;
    private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        //시작 위치 설정
        startLocation = transform.GetChild(1).position;
        endLocation = transform.GetChild(2).position;
        startPosition = startLocation;
        endPosition = endLocation;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        //일정한 속도로 이동
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, endPosition, step);

        //종료 위치에 도달하면 이동 방향은 반대로 변경하여 시작 위치로 되돌아감
        if (Vector2.Distance(transform.position, endPosition) < 0.001f)
        {
            endPosition = startPosition;
            startPosition = transform.position;
        }
    }  
    
}