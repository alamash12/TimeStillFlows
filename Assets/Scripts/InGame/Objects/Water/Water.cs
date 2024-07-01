using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour , IChangeable
{
    Transform waterPivot;
    public List<GameObject> TriggeredBlock = new(); // 물에 닿은 블록을 저장하기 위한 리스트
    Dictionary<Rigidbody2D, Coroutine> activeCoroutines = new Dictionary<Rigidbody2D, Coroutine>(); // 코루틴을 저장하는 용도
    float waterY;
    float buoyancyStrength = 10.0f; // 부력의 세기
    float dampingFactor = 0.96f; // 감쇠 계수
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
    void Init()
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
            SoundManager.Instance.EffectSoundOn("WaterFall");
            TriggeredBlock.Add(collision.gameObject);
            collision.gameObject.layer = 2; // 물에 닿은 블록을 Ignore Raycast로 설정 (점프가 불가능하게)

            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f; //부력 코루틴을 작동시키기 위해 중력을 0으로
            activeCoroutines.Add(rigid, StartCoroutine(Buoyancy(rigid))); // 부력 구현 코루틴 딕셔너리에 저장하며 시작
        }
        if (collision.CompareTag("Player")) // 잠시 테스트로 부력 놔둠
        {
            SoundManager.Instance.EffectSoundOn("WaterFall");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 다시시작
        }
    }
    IEnumerator Buoyancy(Rigidbody2D collision)
    {
        while (true)
        {
            // 물체가 물보다 위에 있을 때 대기
            yield return new WaitWhile(() => collision.transform.position.y > waterY);

            while (collision.transform.position.y < waterY)
            {
                collision.AddForce(new Vector2(0, (waterY - collision.transform.position.y) * buoyancyStrength), ForceMode2D.Force); // 깊이에 따라 강해지는 부력 적용
                collision.velocity = new Vector2(collision.velocity.x, collision.velocity.y * dampingFactor); // 속력 감쇠 적용

                yield return null;
            }
            collision.velocity = new Vector2(collision.velocity.x, 0); // 원하는 높이까지 왔으면 속력을 0으로
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

            TriggeredBlock.Remove(collision.gameObject); 
            collision.gameObject.layer = 0; // 물에서 블록이 나갈때 다시 레이캐스트 가능하도록 변경
        }
    }
}
