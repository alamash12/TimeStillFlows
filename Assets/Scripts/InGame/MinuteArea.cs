using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinuteArea : MonoBehaviour
{
    ObjectContainer objectContainer; // 영역에 들어온 오브젝트를 관리하는 객체
    Vector2 playerPosition;
    float nearestDistance;
    public GameObject nearestObject;
    Rigidbody2D nearestRigid;
    SpriteRenderer nearestObjectSR;
    SpriteRenderer nearestObjectOutlineSR;

    public void Init(ObjectContainer container)
    {
        objectContainer = container;
    }
    public void ChangeState()
    {
        if (nearestObject != null)
        {
            IChangeable changableComponent = nearestObject.GetComponent<IChangeable>();
            if (changableComponent != null)
            {
                Rigidbody2D rigidbody = nearestObject.GetComponent<Rigidbody2D>();
                changableComponent.stateType = StateType.Flow; // 상태를 변경
                objectContainer.triggeredObjectRigid[rigidbody] = changableComponent.stateType; // 딕셔너리에 변경된 상태를 갱신
                rigidbody.WakeUp(); // stay()함수를 돌리기 위해 리지드바디를 깨운다.
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
            IChangeable changableObject = collision.GetComponent<IChangeable>();

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
                        OrderDecision(false);
                    }
                    nearestObject = kvp.Key.gameObject;
                    nearestObjectSR = nearestObject.GetComponent<SpriteRenderer>();
                    nearestObjectOutlineSR = nearestObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    nearestRigid = nearestObject.GetComponent<Rigidbody2D>();
                }
                nearestDistance = (playerPosition - nearestRigid.ClosestPoint(playerPosition)).sqrMagnitude;
            }
        }
        //노란 테두리 표시
        if(nearestObject != null)
        {
            OrderDecision(true);
        }
    }

    private void Update()
    {
        playerPosition = gameObject.transform.parent.position;
    }

    void MinuteAreaClear()
    {
        OrderDecision(false);
        nearestObject = null;
        nearestDistance = Mathf.Infinity;
        nearestRigid = null;
    }
    /// <summary>
    /// 아웃라인을 보여주고, 아웃라인이 나타나는 오브젝트의 sortingorder를 조절해주는 함수
    /// </summary>
    /// <param name="isEnabled">윤곽선의 여부</param>
    /// 바닥 > 물 > 플레이어 = 블록/발판 > 아웃라인
    /// 8      6       4          1          0      <- 각 오브젝트의 sortingorder
    /// nearestObject가 되었을때 아웃라인과 오브젝트 order++
    void OrderDecision(bool isEnabled)
    {
        if (isEnabled) // 윤곽선을 켜줘야하는 경우
        {
            if (nearestObjectSR == null) // 부모 오브젝트에 SpriteRenderer가 없는 경우 ex) 물, 움직이는 발판중 긴것
            {
                for (int i = 3; i < nearestObject.transform.childCount; i++)
                {
                    nearestObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder++;
                }
                nearestObjectOutlineSR.sortingOrder++;
                nearestObjectOutlineSR.enabled = isEnabled;
            }
            else // 부모 오브젝트에 SpriteRenderer가 있는 경우 ex) 위의 경우를 제외한 나머지
            {
                nearestObjectSR.sortingOrder++;
                nearestObjectOutlineSR.sortingOrder++;
                nearestObjectOutlineSR.enabled = isEnabled;
            }
        }
        else // 윤곽선을 꺼줘야하는 경우
        {
            if (nearestObjectSR == null) // 부모 오브젝트에 SpriteRenderer가 없는 경우 ex) 물, 움직이는 발판중 긴것
            {
                for (int i = 3; i < nearestObject.transform.childCount; i++)
                {
                    nearestObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder--;
                }
                nearestObjectOutlineSR.sortingOrder--;
                nearestObjectOutlineSR.enabled = isEnabled;
            }
            else // 부모 오브젝트에 SpriteRenderer가 있는 경우 ex) 위의 경우를 제외한 나머지
            {
                nearestObjectSR.sortingOrder--;
                nearestObjectOutlineSR.sortingOrder--;
                nearestObjectOutlineSR.enabled = isEnabled;
            }
        }
    }
}
