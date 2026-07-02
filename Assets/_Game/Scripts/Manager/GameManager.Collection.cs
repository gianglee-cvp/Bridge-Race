using UnityEngine;
using System.Collections.Generic;
using System;
public partial class GameManager : Singleton<GameManager>
{
    // TODO nên cache collider của character để tối ưu bộ nhớ 
    private Dictionary<Collider, Brick> brickDictionary = new Dictionary<Collider, Brick>();
    private Dictionary<Collider, Character> characterDictionary = new Dictionary<Collider, Character>();

    [SerializeField] public List<Stage> stageList = new List<Stage>();
    public List<Character> listCharacters = new List<Character>();
    public List<String> listTriggerAnimator = new List<String>();
    public List<String> listColorLayerName = new List<String>();



    public Character GetCharacter(Collider collider)
    {
        return characterDictionary[collider];
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
    public Material GetMaterial(ENUM_COLOR color)
    {
        return colorDataSO.GetMaterial(color);
    }


}