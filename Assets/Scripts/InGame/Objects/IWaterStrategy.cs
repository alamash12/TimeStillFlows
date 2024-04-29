using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaterStrategy
{
    void OnTriggerEnter2D(Collider2D collision);
    void OnTriggerExit2D(Collider2D collision);
    void SetWaterY(float waterY);
}
