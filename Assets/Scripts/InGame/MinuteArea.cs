using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinuteArea : MonoBehaviour
{
    List<Rigidbody2D> triggeredObjectRigid = new();
    Vector2 playerPosition;
    float nearestDistance;
    public GameObject nearestObject;


    public void ChangeStrategy(IChangable gameObject)
    {
        gameObject.stateType = StateType.Flow;
    }

    private void Awake()
    {
        nearestDistance = Mathf.Infinity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            //충돌한 오브젝트의 상태를 가져옴
            IChangable changableObject = collision.GetComponent<IChangable>();

            //상태가 Stop인 경우에만 리스트에 추가
            if (changableObject != null && changableObject.stateType == StateType.Stop)
            {
                triggeredObjectRigid.Add(collision.GetComponent<Rigidbody2D>());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            triggeredObjectRigid.Remove(collision.GetComponent<Rigidbody2D>());
            if(collision.gameObject == nearestObject)
            {     
                nearestObject = null;
                nearestDistance = Mathf.Infinity;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (Rigidbody2D rigidbody2d in triggeredObjectRigid)
        {  
            if ((playerPosition - rigidbody2d.ClosestPoint(playerPosition)).sqrMagnitude < nearestDistance) // 플레이어와 가장 가까운 collider의 지점과 가장 가까운 부분을 비교
            {
                nearestObject = rigidbody2d.gameObject;
                //Debug.Log(nearestObject);
            }
            nearestDistance = (playerPosition - nearestObject.GetComponent<Rigidbody2D>().ClosestPoint(playerPosition)).sqrMagnitude;
        }
        //노란 박스 키고 끄는 부분, 임시로 노란색으로 변하는걸로 구현 
        if(nearestObject != null)
        {
             //nearestObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    private void Update()
    {
        playerPosition = gameObject.transform.parent.position;
    }

 
}
