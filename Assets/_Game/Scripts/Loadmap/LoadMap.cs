using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BrickData
{
    public ENUM_COLOR color;
    public Vector3 position;
}
[System.Serializable]
public class StageData
{
    public List<BrickData> bricks = new List<BrickData>();
}
[System.Serializable]
public class MapData
{
    public List<StageData> stages = new List<StageData>();
}
