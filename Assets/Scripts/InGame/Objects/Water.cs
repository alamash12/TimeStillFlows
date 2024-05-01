using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour , IChangable
{
    Transform waterPivot;
    float waterY;
    private void Awake()
    {
        waterPivot = gameObject.transform.GetChild(0);
        waterY = waterPivot.transform.position.y;
        gameObject.AddComponent<WaterFlow>().waterY = waterY;
    }
    public void ChangeState()
    {
        bool isFlow = gameObject.GetComponent<WaterFlow>() != null ? true : false;
        if (isFlow)
        {
            Destroy(gameObject.GetComponent<WaterFlow>());
            gameObject.AddComponent<WaterStop>().waterY = waterY;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.75f);
        }
        else
        {
            Destroy(gameObject.GetComponent<WaterStop>());
            gameObject.AddComponent<WaterFlow>().waterY = waterY;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 0.75f);
        }
    }
}
