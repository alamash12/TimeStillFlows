using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    Transform waterPivot;
    float waterY;
    IWaterStrategy waterStrategy;
    private void Awake()
    {
        waterPivot = gameObject.transform.GetChild(0);
        waterY = waterPivot.transform.position.y;
        FlowStrategy(gameObject);
    }

    public void SetStrategy(IWaterStrategy waterStrategy)
    {
        this.waterStrategy = waterStrategy;
    }

    public void FlowStrategy(GameObject target)
    {
        ClearStrategyComponent(target);
        IWaterStrategy strategy = target.AddComponent<WaterFlow>();
        strategy.SetWaterY(waterY);
        SetStrategy(strategy);
    }

    public void StopStrategy(GameObject target)
    {
        ClearStrategyComponent(target);
        IWaterStrategy strategy = target.AddComponent<WaterStop>();
        strategy.SetWaterY(waterY);
        SetStrategy(strategy);
    }
    public void ClearStrategyComponent(GameObject target)
    {
        Destroy(target.GetComponent<WaterFlow>());
        Destroy(target.GetComponent<WaterStop>());
    }
}
