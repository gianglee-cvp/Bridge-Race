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
            character.OnInit();
        }
    }
    public Vector2 GetVector2XZ(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
}
