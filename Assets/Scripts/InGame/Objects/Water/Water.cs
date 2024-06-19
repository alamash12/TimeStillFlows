using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour , IChangable
{
    Transform waterPivot;
    public List<GameObject> TriggeredBlock = new(); // 물에 닿은 블록을 저장하기 위한 리스트
    Dictionary<Rigidbody2D, Coroutine> activeCoroutines = new Dictionary<Rigidbody2D, Coroutine>(); // 코루틴을 저장하는 용도
    public float waterY { get; set; }
    private StateType _stateType; // 값 저장 필드
    public StateType stateType 
    {
        get {  return _stateType; }
        set
        {
            if (_stateType != value)
            {
                _stateType = value;
                if (stateType == StateType.Flow)
                {
                    ChangeState<WaterStop, WaterFlow>();
                    DecisionSprite(stateType);
                }
                else if (stateType == StateType.Stop)
                {
                    ChangeState<WaterFlow, WaterStop>();
                    DecisionSprite(stateType);
                }
            }
        }
    }
    private void Awake()
    {
        waterPivot = gameObject.transform.GetChild(1);
        waterY = waterPivot.position.y;
        Init();
    }
    public void Init()
    {
        string stateParse = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite.name.Split('_')[2];
        StateType result;
        if (Enum.TryParse(stateParse, out result))
        {
            if (result == StateType.Flow)
            {
                _stateType = StateType.Flow;
                ChangeState<WaterStop, WaterFlow>();
            }
            else if (result == StateType.Stop)
            {
                _stateType = StateType.Stop;
                ChangeState<WaterFlow, WaterStop>();
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
    public void ChangeState<T1,T2>() where T1 : Component where T2 : Component
    {
        Component destroyComponent = gameObject.GetComponent<T1>();
        Component addComponent = gameObject.GetComponent<T2>();

        Destroy(destroyComponent);
        if(addComponent == null)
        {
            addComponent = gameObject.AddComponent<T2>();
        }
    }

    void DecisionSprite(StateType stateType)
    {
        if(stateType == StateType.Flow)
        {
            GameManager.ChangeSprite(gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>(), -1);
            GameManager.ChangeSprite(gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>(), -1);
        }
        else if(stateType == StateType.Stop)
        {
            GameManager.ChangeSprite(gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>(), 1);
            GameManager.ChangeSprite(gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>(), 1);
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            TriggeredBlock.Add(collision.gameObject);
            collision.gameObject.layer = 2; // 물에 닿은 블록을 Ignore Raycast로 설정

            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            activeCoroutines.Add(rigid, StartCoroutine(Buoyancy(rigid)));
        }
        if (collision.CompareTag("Player")) // 잠시 테스트로 부력 놔둠
        {
            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
            activeCoroutines.Add(rigid, StartCoroutine(Buoyancy(rigid)));
            //게임 오버 구현
        }
    }

    IEnumerator Buoyancy(Rigidbody2D collision)
    {
        while (true)
        {
            yield return new WaitWhile(() => collision.transform.position.y > waterY); // 물체가 waterY보다 밑에 있을때부터 코루틴 시작
            while (collision.transform.position.y < waterY)
            {
                collision.velocity = new Vector2(0, Mathf.Clamp((collision.velocity.y + 0.07f), -10, 5));
                yield return null;
            }
            collision.velocity = new Vector2(0, 0);
            yield return new WaitWhile(() => collision.velocity.y >= 0); // 물체가 아래로 떨어지기 전까지 대기상태
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
        if (rigid != null && activeCoroutines.ContainsKey(rigid))
        {
            StopCoroutine(activeCoroutines[rigid]);
            activeCoroutines.Remove(rigid);
            rigid.gravityScale = 1f;

            TriggeredBlock.Remove(collision.gameObject); // 물에서 블록이 나갈때 다시 레이캐스트 가능하도록 변경
            collision.gameObject.layer = 0;
        }
    }
}
