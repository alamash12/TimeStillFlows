using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDefine
{
    public enum ObjectSprite // Flow , Stop 순으로 정렬해야 스프라이트 변경 가능
    {
        Block_RockFlow,
        Block_RockStop,
        Block_WoodFlow,
        Block_WoodStop,
        Water_SurfaceFlow,
        Water_SurfaceStop,
        Water_InsideFlow,
        Water_InsideStop,
        LaserBodyFlow,
        LaserBodyStop,
        LaserLightFlow,
        LaserLightStop,
    }
}
