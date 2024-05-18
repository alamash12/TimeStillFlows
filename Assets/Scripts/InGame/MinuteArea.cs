using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinuteArea : MonoBehaviour
{
    ObjectContainer objectContainer; // 영역에 들어온 오브젝트를 관리하는 객체
    Vector2 playerPosition;
    float nearestDistance;
    GameObject nearestObject;
    Rigidbody2D nearestRigid;

    public void Initialize(ObjectContainer container)
    {
        objectContainer = container;
    }
    public void ChangeState()
    {
        if (nearestObject != null)
        {
            IChangable changableComponent = nearestObject.GetComponent<IChangable>();
            if (changableComponent != null)
            {
                changableComponent.stateType = StateType.Flow; // 상태를 변경
                objectContainer.triggeredObjectRigid[nearestObject.GetComponent<Rigidbody2D>()] = changableComponent.stateType; // 딕셔너리에 변경된 상태를 갱신
                MinuteAreaClear(); // nearestObject의 상태가 변경되었기 때문에 새로운 nearestObject를 받아오기 위해 초기화
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

            if (changableObject != null)
            {
                if(objectContainer.triggeredObjectRigid.ContainsKey(collision.GetComponent<Rigidbody2D>()) == false) // 같은 값이 없는지 체크
                    objectContainer.triggeredObjectRigid.Add(collision.GetComponent<Rigidbody2D>(), changableObject.stateType);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            objectContainer.triggeredObjectRigid.Remove(collision.GetComponent<Rigidbody2D>());
            if(collision.gameObject == nearestObject) // 영역내에서 모든 오브젝트가 사라졌을 경우
            {
                MinuteAreaClear();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (KeyValuePair<Rigidbody2D,StateType> kvp in objectContainer.triggeredObjectRigid)
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
                    //Debug.Log(nearestObject);
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
