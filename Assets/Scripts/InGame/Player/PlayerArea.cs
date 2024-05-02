using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    List<Rigidbody2D> triggeredObject = new();
    Vector2 playerPosition;
    float nearestDistance;
    public GameObject nearestObject;
    IChangable changable;

    public void ChangeStrategy(IChangable gameObject)
    {
        gameObject.ChangeState();
    }
    private void Awake()
    {
        nearestDistance = Mathf.Infinity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            triggeredObject.Add(collision.GetComponent<Rigidbody2D>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            triggeredObject.Remove(collision.GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (Rigidbody2D rigidbody2d in triggeredObject)
        {
            if ((playerPosition - rigidbody2d.ClosestPoint(playerPosition)).sqrMagnitude < nearestDistance) // 플레이어와 가장 가까운 collider의 지점과 가장 가까운 부분을 비교
            {
                nearestObject = rigidbody2d.gameObject;
                //Debug.Log(nearestObject);
                //노란 박스 키고 끄는 부분
            }
            nearestDistance = (playerPosition - nearestObject.GetComponent<Rigidbody2D>().ClosestPoint(playerPosition)).sqrMagnitude;
        }
    }
    private void Update()
    {
        playerPosition = gameObject.transform.parent.position;
    }
}
