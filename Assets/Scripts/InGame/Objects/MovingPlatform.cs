using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.XR;

public class MovingPlatform : MonoBehaviour, IChangable
{
    private StateType _stateType;

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
            ChangeSprite(addComponent);
        }
    }

    void ChangeSprite(Component component)
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

}
