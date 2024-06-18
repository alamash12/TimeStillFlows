using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.XR;

public class MovingPlatform : MonoBehaviour, IChangable
{
    private StateType _stateType;
    private BoxCollider2D boxCollider;
    private Dictionary<Rigidbody2D, Coroutine> followParent = new Dictionary<Rigidbody2D, Coroutine>(); //자식의 코루틴을 저장 

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
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        stateType = StateType.Stop;
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
            DecisionSprite(addComponent);
        }
    }
     
    void DecisionSprite(Component component)
    {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();


        if (gameObject.GetComponent<SpriteRenderer>() != null) //spriteRender이 오브젝트에 있다면
        {
            spriteRenderers.Add(gameObject.GetComponent<SpriteRenderer>());
            Debug.Log("부모 스프라이트");
        }

        else //spiteRender이 오브젝트의 자식에 있다면 
        {    
            for (int i = 3; i < 6; i++)
            {
                //오브젝트의 자식의 spriteRenderer을 저장
                spriteRenderers.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
                Debug.Log("자식스프라이트");
            }
            
   
        }


        if(component == GetComponent<MovingPlatformFlow>())
        {
           foreach(SpriteRenderer spriteRenderer in spriteRenderers)
           {
                GameManager.ChangeSprite(spriteRenderer, -1);
           }
           
        }
        else if (component == GetComponent<MovingPlatformStop>()) 
        {

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                GameManager.ChangeSprite(spriteRenderer, 1);
            }
        }
        else
        {
            Debug.LogError("Not Available Component");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //레이저와 충돌하는건 나중에 구현
        if (collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Player"))
        {
            if(CheckCollision(collision))
            {
                GameObject childObject = collision.gameObject;
                Rigidbody2D childRigid = childObject.GetComponent<Rigidbody2D>();

                if (childRigid != null && !followParent.ContainsKey(childRigid))
                {
                    collision.transform.SetParent(transform);
                    //코루틴 시작
                    if (collision.gameObject.CompareTag("Object"))
                    {
                        followParent.Add(childRigid, StartCoroutine(childObject.GetComponent<Block>().FollowParent(transform.position)));
                    }
                    else if (collision.gameObject.CompareTag("player"))
                    {
                        followParent.Add(childRigid, StartCoroutine(childObject.GetComponent<Player>().FollowParent(transform.position)));
                    }
                   
                }
                else
                {
                    Debug.Log("there is no Rigidbody");
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //물체가 MovingPlatform에서 떠났을 때 부모를 초기화하여 원래 상태로 되돌림
        collision.transform.SetParent(null);


        Rigidbody2D childRigid = collision.gameObject.GetComponent<Rigidbody2D>();

        //코루틴 종료
        if (followParent.ContainsKey(childRigid))
        {
            StopCoroutine(followParent[childRigid]);
            followParent.Remove(childRigid);
        }
    }

    private  bool CheckCollision(Collision2D collision)
    {
        // BoxCollider2D의 윗면의 Y 위치
        float topY = boxCollider.bounds.max.y;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // 충돌 지점의 Y 위치가 BoxCollider2D의 윗면과 같은지 확인
            if (contact.point.y + 0.05 >= topY)
            {
                return true;
            }
        }

        return false;
    }

}
