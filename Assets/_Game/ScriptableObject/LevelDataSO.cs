using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject
{
    public List<StageData> stages = new();
    public PlayerData player = new PlayerData(); 
    public List<EnemyData> listEnemy = new List<EnemyData>();
    public List<WinPos> winPos = new List<WinPos>(); 

    public void Clear()
    {
        stages.Clear();
    }

    public void AddStage(StageData stage)
    {
        stages.Add(stage);
    }
    public WinPos GetPosAndRot(int seed)
    {
        int index = seed - 1; 
        if(index < 0 || index > winPos.Count)
        {
            Debug.LogError("winPos"); 
        }
        return winPos[index]; 
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
[System.Serializable]
public class WinPos
{
    public Vector3 position; 
    public Vector3 rotation;
}