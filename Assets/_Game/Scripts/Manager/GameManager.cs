using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    [SerializeField] public ColorDataSO colorDataSO;
    public MapManager mapManager;
    public Player player;
    void Awake()
    {
        mapManager.LoadMap(stageList);
        foreach (var character in listCharacters)
        {
            characterDictionary.Add(character.characterCollider, character);
        }
    }

}
