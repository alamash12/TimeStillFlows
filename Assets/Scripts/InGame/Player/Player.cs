using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    ObjectContainer container = new();
    MinuteArea minuteArea;
    HourArea hourArea;
    private void Awake()
    {
        minuteArea = GetComponentInChildren<MinuteArea>();
        hourArea = GameObject.Find("HourArea").GetComponent<HourArea>();
    }
    private void Start()
    {
        minuteArea.Initialize(container);
        hourArea.Initialize(container);
    }
    //player의 위치 반환
    public Vector3 GetLocation()
    {
        return gameObject.GetComponent<Transform>().position;
    }

    //player의 위치 조정 
    public void SetLocation(Vector3 location)
    {
        gameObject.GetComponent<Transform>().position = location;
    }
}
