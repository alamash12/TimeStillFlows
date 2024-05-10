using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourArea : MonoBehaviour
{
    public List<GameObject> triggeredObject = new();
    MinuteArea minuteArea;
    private void Awake()
    {
        minuteArea = GameObject.Find("minuteArea").GetComponent<MinuteArea>();
    }
    public void ChangeState()
    {
        if (triggeredObject != null)
        {
            foreach (GameObject gameObject in triggeredObject)
            {
                IChangable changableComponent = gameObject.GetComponent<IChangable>();
                Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
                if (changableComponent != null)
                {
                    changableComponent.stateType = StateType.Stop;
                    if(minuteArea.triggeredObjectRigid.ContainsKey(rigidbody2D))
                    {
                        minuteArea.triggeredObjectRigid[rigidbody2D] = changableComponent.stateType;
                        rigidbody2D.WakeUp();
                    }
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            triggeredObject.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            triggeredObject.Remove(collision.gameObject);
        }
    }
}
