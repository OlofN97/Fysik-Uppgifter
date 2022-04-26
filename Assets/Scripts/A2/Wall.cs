using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallPosition { top, bottom, left, right }

public class Wall : MonoBehaviour
{
    public WallPosition position;
    public float restitutionCoefficient;

    public int CheckWall()
    {
        switch(position)
        {
            case WallPosition.top:
                return 1;
            case WallPosition.bottom:
                return 2;
            case WallPosition.left:
                return 3;
            case WallPosition.right:
                return 4;
            default:
                return 0;
        }
    }
}
