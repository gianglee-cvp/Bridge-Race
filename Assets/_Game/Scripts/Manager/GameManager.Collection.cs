using UnityEngine;
using System.Collections.Generic;
using System;
public partial class GameManager : Singleton<GameManager>
{
    // TODO nên cache collider của character để tối ưu bộ nhớ 
    private Dictionary<Collider, Character> characterDictionary = new Dictionary<Collider, Character>();

    [SerializeField] public List<Stage> stageList = new List<Stage>();    
    public List<Character> listCharacters = new List<Character>();
    public List<String> listTriggerAnimator = new List<String>();
    public List<String> listColorLayerName = new List<String>();
    public List<Level> listLevels = new List<Level>();



    public Character GetCharacter(Collider collider)
    {
        return characterDictionary[collider];
    }
    public Material GetMaterial(ENUM_COLOR color)
    {
        return colorDataSO.GetMaterial(color);
    }
    public void AddListLevel()
    {
        Queue<GameUnit> levelQueue = SimplePool.poolInstance[PoolType.Level].GetInactive();
        foreach(var level in levelQueue)
        {
            listLevels.Add((Level)level);
        }
    }


}
