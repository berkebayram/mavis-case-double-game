using UnityEngine;
using System;

[Serializable]
public struct BaloonRoutePiece 
{
    public BaloonRouteType RouteType;
    public Vector3 Start;
    public Vector3 End;
    public Vector3 MiddlePoint;
}
