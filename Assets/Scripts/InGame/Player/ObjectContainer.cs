using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContainer // 영역에 들어온 오브젝트를 관리해주는 클래스
{
    public List<GameObject> triggeredObject = new(); // hourArea에서 사용되는 리스트
    public Dictionary<Rigidbody2D, StateType> triggeredObjectRigid = new(); // minuteArea에서 사용되는 리스트
}
