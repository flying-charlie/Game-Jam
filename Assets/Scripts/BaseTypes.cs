using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputSet
{
    public float x;
    public float y;
    public bool firing;
    public float mouseDirection;
    public bool mouseOnScreen;
}

public struct TileUpdateData
{
    public InputSet inputs;
}