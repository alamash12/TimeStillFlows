using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour , IChangable
{
    Transform waterPivot;
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
                }
                else if (stateType == StateType.Stop)
                {
                    ChangeState<WaterFlow, WaterStop>();
                }
            }
        }
    }
    private void Awake()
    {
        waterPivot = gameObject.transform.GetChild(1);
        waterY = waterPivot.position.y;
        stateType = StateType.Stop; // 초기상태 flow로 지정
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
            DecisionSprite(addComponent);
        }
    }

    void DecisionSprite(Component component)
    {
        if(component == GetComponent<WaterFlow>())
        {
            GameManager.ChangeSprite(gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>(), -1);
            GameManager.ChangeSprite(gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>(), -1);
        }
        else if(component == GetComponent<WaterStop>())
        {
            GameManager.ChangeSprite(gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>(), 1);
            GameManager.ChangeSprite(gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>(), 1);
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }
}
