using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IChangable
{
    GameObject laserBody;
    private StateType _stateType; //값 저장 필드
    public StateType stateType
    {
        get { return _stateType; }
        set
        {
            if (_stateType != value)
            {
                _stateType = value;
                if (stateType == StateType.Flow)
                {
                    ChangeState<LaserStop, LaserFlow>();
                }
                else if (stateType == StateType.Stop)
                {
                    ChangeState<LaserFlow, LaserStop>();
                }
            }
        }
    }
    void Awake()
    {
        stateType = StateType.Flow;
        laserBody = gameObject.transform.parent.gameObject; // 레이저 바디를 받아옴
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
            addComponent = gameObject.AddComponent<T2>();
            DecisionSprite(addComponent);
        }
    }
    void DecisionSprite(Component component) // 레이저 바디와 연동해서 바꿔야 한다.
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (component == GetComponent<LaserFlow>()) // flow면 count를 1로
        {
            GameManager.ChangeSprite(spriteRenderer, 1);
        }
        else if (component == GetComponent<LaserStop>()) // stop이면 count를 -1로
        {
            GameManager.ChangeSprite(spriteRenderer, -1);
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }
}
