using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourArea : MonoBehaviour
{
    ObjectContainer objectContainer; // 영역에 들어온 오브젝트를 관리하는 객체
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        //SizeDecision();
    }
    public void Init(ObjectContainer container)
    {
        objectContainer = container;
    }
    public void ChangeState()
    {
        if (objectContainer.triggeredObject != null)
        {
            foreach (GameObject gameObject in objectContainer.triggeredObject)
            {
                IChangeable changableComponent = gameObject.GetComponent<IChangeable>();
                Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
                if (changableComponent != null)
                {
                    changableComponent.stateType = StateType.Stop; // 상태를 변경
                    if(objectContainer.triggeredObjectRigid.ContainsKey(rigidbody2D))
                    {
                        objectContainer.triggeredObjectRigid[rigidbody2D] = changableComponent.stateType; // minuteArea의 상태를 변경
                        rigidbody2D.WakeUp(); // OntriggerStay를 실행시키기 위해
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            objectContainer.triggeredObject.Add(collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            objectContainer.triggeredObject.Remove(collision.gameObject);
        }
    }
}
