using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.XR;

public class MovingPlatform : MonoBehaviour, IChangeable
{
    private StateType _stateType;
    private BoxCollider2D boxCollider;
    private Dictionary<Rigidbody2D, Coroutine> followParent = new Dictionary<Rigidbody2D, Coroutine>(); //자식의 코루틴을 저장 

    [SerializeField]
    public Vector2 startLocation;
    public Vector2 endLocation;

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
                    DecisionSprite(stateType);
                }
                else if(stateType == StateType.Stop)
                {
                    ChangeState<MovingPlatformFlow, MovingPlatformStop>();
                    DecisionSprite(stateType);
                }
            }
        }
    }

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        Init();
    }
    public void Init()
    {
        string stateParse;
        if (gameObject.GetComponent<SpriteRenderer>() != null) //spriteRender이 오브젝트에 있다면
        {
            stateParse = gameObject.GetComponent<SpriteRenderer>().sprite.name.Split('_')[2];
        }
        else //spiteRender이 오브젝트의 자식에 있다면 
        {
            stateParse = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite.name.Split('_')[2];
        }
        StateType result;
        if (Enum.TryParse(stateParse, out result))
        {
            if (result == StateType.Flow)
            {
                _stateType = StateType.Flow;
                ChangeState<MovingPlatformStop, MovingPlatformFlow>();
            }
            else if (result == StateType.Stop)
            {
                _stateType = StateType.Stop;
                ChangeState<MovingPlatformFlow, MovingPlatformStop>();
            }
            else
            {
                Debug.LogError(gameObject.name + "이 제대로 초기화되지 않았습니다.");
            }
        }
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
        }
    }
     
    void DecisionSprite(StateType stateType)
    {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();


        if (gameObject.GetComponent<SpriteRenderer>() != null) //spriteRender이 오브젝트에 있다면
        {
            spriteRenderers.Add(gameObject.GetComponent<SpriteRenderer>());
            Debug.Log("부모 스프라이트");
        }

        else //spiteRender이 오브젝트의 자식에 있다면 
        {    
            for (int i = 1; i < transform.childCount; i++)
            {
                //오브젝트의 자식의 spriteRenderer을 저장
                spriteRenderers.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
                Debug.Log("자식스프라이트");
            }
        }

        if(stateType == StateType.Flow)
        {
           foreach(SpriteRenderer spriteRenderer in spriteRenderers)
           {
                GameManager.ChangeSprite(spriteRenderer, -1);
           }
           
        }
        else if (stateType == StateType.Stop) 
        {

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                GameManager.ChangeSprite(spriteRenderer, 1);
            }
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //레이저와 충돌하는건 나중에 구현
        if (collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Player"))
        {
            if(CheckCollision(collision))
            {
                GameObject childObject = collision.gameObject;
                Rigidbody2D childRigid = childObject.GetComponent<Rigidbody2D>();

                if (childRigid != null && !followParent.ContainsKey(childRigid))
                {
                 
                    //코루틴 시작
                    if (collision.gameObject.CompareTag("Object"))
                    {
                        followParent.Add(childRigid, StartCoroutine(childObject.GetComponent<Block>().FollowParent(transform.position,transform)));
                    }
                    else if (collision.gameObject.CompareTag("Player"))
                    {
                        followParent.Add(childRigid, StartCoroutine(childObject.GetComponent<Player>().FollowParent(transform.position, transform)));
                    }
                   
                }
                else
                {
                    Debug.Log("there is no Rigidbody");
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //물체가 MovingPlatform에서 떠났을 때 부모를 초기화하여 원래 상태로 되돌림

        Rigidbody2D childRigid = collision.gameObject.GetComponent<Rigidbody2D>();

        //코루틴 종료
        if (followParent.ContainsKey(childRigid))
        {
            StopCoroutine(followParent[childRigid]);
            followParent.Remove(childRigid);
        }
    }
    

    private bool CheckCollision(Collision2D collision)
    {
        // BoxCollider2D의 윗면의 Y 위치
        float topY = boxCollider.bounds.max.y;
        float buttomY = collision.gameObject.GetComponent<BoxCollider2D>().bounds.min.y;

        if (buttomY - topY >= -0.1)
        {
            return true;
        }
        return false;
    }

}
