using System;
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
}
