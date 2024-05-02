using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourArea : MonoBehaviour
{
    public List<GameObject> triggeredObject = new();
    public void ChangeStrategy(IChangable gameObject)
    {
        gameObject.ChangeState<WaterFlow, WaterStop>(); 
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
