using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject
{
    public List<StageData> stages = new();
    public PlayerData player = new PlayerData(); 
    public List<EnemyData> listEnemy = new List<EnemyData>();

    public void Clear()
    {
        stages.Clear();
    }

    public void AddStage(StageData stage)
    {
        stages.Add(stage);
    }
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
[System.Serializable]
public class PlayerData
{
    public ENUM_COLOR color;
    public Vector3 position;
}
[System.Serializable]
public class EnemyData
{
    public ENUM_COLOR color; 
    public Vector3 position;
}