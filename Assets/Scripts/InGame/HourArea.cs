using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourArea : MonoBehaviour
{
    public List<GameObject> triggeredObject = new();
    public void ChangeStrategy(IChangable gameObject)
    {
        gameObject.stateType = StateType.Stop;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") || collision.CompareTag("Block") || collision.CompareTag("MovingPlatform") || collision.CompareTag("Laser") || collision.CompareTag("Water"))
        {
            triggeredObject.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") || collision.CompareTag("Block") || collision.CompareTag("MovingPlatform") || collision.CompareTag("Laser") || collision.CompareTag("Water"))
        {
            triggeredObject.Remove(collision.gameObject);
        }
    }
}
