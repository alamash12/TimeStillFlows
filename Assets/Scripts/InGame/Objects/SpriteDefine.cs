using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDefine
{
    public enum ObjectSprite // Flow , Stop 순으로 정렬해야 스프라이트 변경 가능
    {
        Block_Rock_Flow,
        Block_Rock_Stop,
        Block_Wood_Flow,
        Block_Wood_Stop,
        Water_Surface_Flow,
        Water_Surface_Stop,
        Water_Inside_Flow,
        Water_Inside_Stop,
    }
}
