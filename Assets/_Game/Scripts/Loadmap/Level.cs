using UnityEngine;
using System.Collections.Generic;

public class Level : GameUnit
{
    [SerializeField] public List<Stage> stageList = new List<Stage>();
    [SerializeField] public LevelDataSO levelDataSO;

    public void OnInit()
    {
        
    }
    public void OnEnd()
    {
        foreach(var stage in stageList)
        {
            stage.OnEnd();
        }
    }
}