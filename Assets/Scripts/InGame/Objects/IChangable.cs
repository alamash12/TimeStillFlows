using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChangable
{
    public StateType stateType { get; set; }
    public void ChangeState<T1, T2>() where T1 : Component where T2 : Component;
}
