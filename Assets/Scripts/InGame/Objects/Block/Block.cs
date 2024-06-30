using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.XR;

public class Block : MonoBehaviour, IChangeable
{
    private StateType _stateType;
    private BoxCollider2D boxCollider;
    private Dictionary<Rigidbody2D, Coroutine> followParent = new Dictionary<Rigidbody2D, Coroutine>(); //자식의 코루틴을 저장 

    float previousX;
    bool isDragging = false;
    public StateType stateType
    {
        get
        {
            return _stateType;
        }
        set
        {
            if (stateType != value)
            {
                _stateType = value;
                if (stateType == StateType.Flow)
                {
                    ChangeState<BlockStop, BlockFlow>();
                    DecisionSprite(stateType);
                }
                else if (stateType == StateType.Stop)
                {
                    ChangeState<BlockFlow, BlockStop>();
                    DecisionSprite(stateType);
                }
            }
        }
    }

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        previousX = gameObject.transform.position.x;
        Init();
    }

    public IEnumerator FollowParent(Vector3 lastPosition, Transform parent) //부모를 따라 움직임
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        while (true)
        {
            yield return wait;
            Vector3 currentPosition = parent.position;
            Vector3 movementDelta = currentPosition - lastPosition;


            transform.position += movementDelta;
            lastPosition = currentPosition;

        }
    }

    void Init()
    {
        string stateParse = gameObject.GetComponent<SpriteRenderer>().sprite.name.Split('_')[2];
        StateType result;
        if (Enum.TryParse(stateParse, out result))
        {
            if (result == StateType.Flow)
            {
                _stateType = StateType.Flow;
                ChangeState<BlockStop, BlockFlow>();
            }
            else if (result == StateType.Stop)
            {
                _stateType = StateType.Stop;
                ChangeState<BlockFlow, BlockStop>();
            }
            else
            {
                Debug.LogError(gameObject.name + "이 제대로 초기화되지 않았습니다.");
            }
        }
    }

    /// <summary>
    /// 상태를 변화시키는 함수
    /// </summary>
    /// <typeparam name="T1">변화하기 전의 상태</typeparam>
    /// <typeparam name="T2">변화한 후의 상태</typeparam>
    public void ChangeState<T1, T2>() where T1 : Component where T2 : Component
    {
        Component destroyComponent = gameObject.GetComponent<T1>();
        Component addComponent = gameObject.GetComponent<T2>();

        Destroy(destroyComponent);
        if (addComponent == null)
        {
            gameObject.AddComponent<T2>();
        }
    }

    void DecisionSprite(StateType stateType)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (stateType == StateType.Flow)
        {
            GameManager.ChangeSprite(spriteRenderer, -1);
        }
        else if (stateType == StateType.Stop)
        {
            GameManager.ChangeSprite(spriteRenderer, 1);
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject childObject = collision.gameObject;

        if ((childObject.CompareTag("Object") || childObject.CompareTag("Player")) && CheckCollision(collision))
        {
            Rigidbody2D childRigid = childObject.GetComponent<Rigidbody2D>();

            if (childRigid != null && !followParent.ContainsKey(childRigid))
            {
                //코루틴 시작
                if (collision.gameObject.CompareTag("Object") && collision.gameObject.GetComponent<IChangeable>().stateType == StateType.Flow) //this: block의 부모 역할 
                {
                    followParent.Add(childRigid, StartCoroutine(childObject.GetComponent<Block>().FollowParent(transform.position, transform)));
                }
                else if (collision.gameObject.CompareTag("Player"))//this: player의 부모 역할 
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        //물체가 block에서 떠났을 때 부모를 초기화하여 원래 상태로 되돌림
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

    private void FixedUpdate() // 드래그를 하려할때 
    {
        float currentX = gameObject.transform.position.x;
        if (Mathf.Abs(currentX - previousX) >= 0.03f)
        {
            if (!isDragging)
            {
                isDragging = true;
                StartCoroutine(SFXPlay());
            }
        }
        else
        {
            if (isDragging)
            {
                isDragging = false;
                StopCoroutine(SFXPlay());
                SoundManager.Instance.EffectSoundOff();
            }
        }
        previousX = currentX;
    }

    IEnumerator SFXPlay()
    {
        while (isDragging)
        {
            SoundManager.Instance.EffectSoundOn("BlockPush");
            yield return new WaitForSeconds(10f);
        }
    }
}
