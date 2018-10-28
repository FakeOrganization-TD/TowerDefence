using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : IFTDMapObject {

    public TerrainType TerrainType;
    public Direction Direction;
    private int _height;

    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }
    public Terrain(TerrainType terrainType,int height)
    {
        TerrainType = terrainType;
        Height = height;
    }
}



public enum TerrainType
{
    Path,
    Normal,
    TrashBin,
    BlackHole,
    Cristal,
    DestroyedTurret
}
public enum Direction
{
    Up, Down, Left, Right
}
