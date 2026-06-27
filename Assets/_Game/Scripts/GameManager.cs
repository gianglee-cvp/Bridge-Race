using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public ColorDataSO colorDataSO;
    public MapManager mapManager;
    public Player player;
    // TODO nên cache collider của character để tối ưu bộ nhớ 
    private Dictionary<Collider, Brick> brickDictionary = new Dictionary<Collider, Brick>();
    public Dictionary<Collider, Character> characterDictionary = new Dictionary<Collider, Character>();
    public List<Character> listCharacters = new List<Character>();
    [SerializeField] public List<GameObject> stageList = new List<GameObject>(); // lưu danh sách các stage
    void Awake()
    {
        mapManager.LoadMap(stageList);
        foreach (var character in listCharacters)
        {
            characterDictionary.Add(character.characterCollider, character);
        }
    }
    public void RegisterBrick(Collider collider, Brick brick)
    {
        if (!brickDictionary.ContainsKey(collider))
        {
            brickDictionary.Add(collider, brick);
        }
    }
    //TODO : khong remove collider , chi can enable false collider thoi, sau do dung on relesa va in collect 
    public void UnregisterBrick(Collider collider)
    {
        if (brickDictionary.ContainsKey(collider))
        {
            brickDictionary.Remove(collider);
        }
    }

    public Brick GetBrick(Collider collider)
    {
        return brickDictionary[collider];
    }
}
