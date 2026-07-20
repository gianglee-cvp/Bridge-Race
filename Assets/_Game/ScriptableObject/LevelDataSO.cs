using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public List<StageData> stages = new();
}

[System.Serializable]
public class StageData
{
    public List<BrickData> bricks = new();
}

[System.Serializable]
public class BrickData
{
    public ENUM_COLOR color;
    public Vector3 position;
}