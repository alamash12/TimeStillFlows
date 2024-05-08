using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.XR;

public class Block : MonoBehaviour, IChangable
{
    private StateType _stateType;

    public StateType stateType
    {
        get { return _stateType; }
        set
        {
            if (stateType != value)
            {
                _stateType = value;
                if (stateType == StateType.Flow)
                {
                    ChangeState<BlockStop, BlockFlow>();
                }
                else if (stateType == StateType.Stop)
                {
                    ChangeState<BlockFlow, BlockStop>();
                }
            }
        }
    }

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }



    public void ChangeState<T1,T2>() where T1:Component where T2:Component
    {
        Component destroyComponent = gameObject.GetComponent<T1>();
        Component addComponent = gameObject.GetComponent<T2>();

        Destroy(destroyComponent);
        if(addComponent != null )
        {
            addComponent = gameObject.AddComponent<T2>();
            ChangeSprite(addComponent);
        }
    }

    void ChangeSprite(Component component)
    {
        if(component == GetComponent<BlockFlow>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if(component == GetComponent<BlockStop>())
        {
            gameObject.GetComponent <SpriteRenderer>().color = Color.gray;
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }
}
