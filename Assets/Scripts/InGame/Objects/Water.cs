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
        ChangeState<WaterStop, WaterFlow>(); // 초기상태 flow로 지정
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
            ChangeSprite(addComponent);
        }
    }

    void ChangeSprite(Component component)
    {
        if(component == GetComponent<WaterFlow>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 0.75f);
        }
        else if(component == GetComponent<WaterStop>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.75f); // 임시로 색깔만 바꾸기
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }
}
