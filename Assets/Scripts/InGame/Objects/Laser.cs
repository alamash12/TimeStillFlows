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
        ChangeState<LaserStop, LaserFlow>();
        laserBody = gameObject.transform.parent.gameObject; // 레이저 바디를 받아옴
        Debug.Log(laserBody);
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
            ChangeSprite(addComponent);
        }
    }
    void ChangeSprite(Component component) // 레이저 바디와 연동해서 바꿔야 한다.
    {
        if (component == GetComponent<LaserFlow>())
        {
            Debug.Log("Flow");
        }
        else if (component == GetComponent<LaserStop>())
        {
            Debug.Log("Stop");
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }
}
