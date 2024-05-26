using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.XR;

public class MovingPlatform : MonoBehaviour, IChangable
{
    private StateType _stateType;
    private BoxCollider2D boxCollider;

    public StateType stateType
    {
        get { return _stateType; }
        set
        {
            if(stateType != value)
            {
                _stateType = value;
                if(stateType == StateType.Flow)
                {
                    ChangeState<MovingPlatformStop, MovingPlatformFlow>();
                }
                else if(stateType == StateType.Stop)
                {
                    ChangeState<MovingPlatformFlow, MovingPlatformStop>();
                }
            }
        }
    }

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        stateType = StateType.Stop;
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    //스크립트를 활성, 비활성
    public void ChangeState<T1, T2>() where T1:Component where T2:Component
    {
        Component destroyComponent = gameObject.GetComponent<T1>();
        Component addComponent = gameObject.GetComponent<T2>();

        Destroy(destroyComponent);
        if (addComponent == null)
        {
            addComponent = gameObject.AddComponent<T2>();
            DecisionSprite(addComponent);
        }
    }

    void DecisionSprite(Component component)
    {
        if(component == GetComponent<MovingPlatformFlow>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (component == GetComponent<MovingPlatformStop>()) 
        {
            gameObject.GetComponent <SpriteRenderer>().color = Color.gray;
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }

    //Player가 이동발판위에 '안착'하는 경우 아래 코드를 추가

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //레이저와 충돌하는건 나중에 구현
        if (collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Player"))
        {
            if(CheckCollision(collision))
            {
                //레이저와 충돌하는건 나중에 구현
                Rigidbody2D objectRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                if (objectRigidbody != null)
                {
                    //MovingPlatform의 위치에 따라 물체의 위치 조정
                    collision.transform.SetParent(transform);
                }
            }
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //물체가 MovingPlatform에서 떠났을 때 부모를 초기화하여 원래 상태로 되돌림
        collision.transform.SetParent(null);
    }

    private  bool CheckCollision(Collision2D collision)
    {
        // BoxCollider2D의 윗면의 Y 위치
        float topY = boxCollider.bounds.max.y;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.Log("충돌 감지");
            // 충돌 지점의 Y 위치가 BoxCollider2D의 윗면과 같은지 확인
            if (Mathf.Approximately(contact.point.y, topY))
            {
                Debug.Log("접촉이 BoxCollider2D의 윗면에서 발생했습니다.");
                return true;
            }
        }

        return false;
    }

}