using UnityEngine;
using System.Collections.Generic;

public class Level : GameUnit
{
    [SerializeField] public List<Stage> stageList = new List<Stage>();
    [SerializeField] public LevelDataSO levelDataSO;

    public void Load()
    {
        gameObject.SetActive(true);

        int stageCount = stageList.Count;
        for (int i = 0; i < stageCount; i++)
        {
            stageList[i].Load(levelDataSO.stages[i]);
        }
    }

    public void Unload()
    {
        foreach(var stage in stageList)
        {
            stage.OnEnd();
        }

        gameObject.SetActive(false);
    }
}
