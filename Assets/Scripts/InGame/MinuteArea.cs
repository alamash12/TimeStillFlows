using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinuteArea : MonoBehaviour
{
    public Dictionary<Rigidbody2D,StateType> triggeredObjectRigid = new();
    Vector2 playerPosition;
    float nearestDistance;
    GameObject nearestObject;
    Rigidbody2D nearestRigid;

    public void ChangeState()
    {
        if (nearestObject != null)
        {
            IChangable changableComponent = nearestObject.GetComponent<IChangable>();
            if (changableComponent != null)
            {
                changableComponent.stateType = StateType.Flow;
                triggeredObjectRigid[nearestObject.GetComponent<Rigidbody2D>()] = changableComponent.stateType;
                MinuteAreaClear();
            }
        }
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
            if (changableObject != null)
            {
                triggeredObjectRigid.Add(collision.GetComponent<Rigidbody2D>(), changableObject.stateType);
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
                MinuteAreaClear();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (KeyValuePair<Rigidbody2D,StateType> kvp in triggeredObjectRigid)
        {
            if(kvp.Value == StateType.Stop)
            {
                if ((playerPosition - kvp.Key.ClosestPoint(playerPosition)).sqrMagnitude < nearestDistance) // 플레이어와 가장 가까운 collider의 지점과 가장 가까운 부분을 비교
                {
                    if(nearestObject != null)
                    {
                        Transform border = nearestObject.GetComponent<Transform>().GetChild(0);
                        border.GetComponent<SpriteRenderer>().enabled = false;
                    }
                 

                    nearestObject = kvp.Key.gameObject;
                    nearestRigid = nearestObject.GetComponent<Rigidbody2D>();
                }
                nearestDistance = (playerPosition - nearestRigid.ClosestPoint(playerPosition)).sqrMagnitude;
            }
        }
        //노란 테두리 표시
        if(nearestObject != null)
        {
            Transform border = nearestObject.GetComponent<Transform>().GetChild(0);
            border.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void Update()
    {
        playerPosition = gameObject.transform.parent.position;
    }

    public void MinuteAreaClear()
    {
        Transform border = nearestObject.GetComponent<Transform>().GetChild(0);
        border.GetComponent<SpriteRenderer>().enabled = false;

        nearestObject = null;
        nearestDistance = Mathf.Infinity;
        nearestRigid = null;
    }
}
