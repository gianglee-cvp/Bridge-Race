using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BlockWallAndVisual
{
    public Door door;
    public Collider blockCollier;
}

public class DoorControl : MonoBehaviour
{
    [SerializeField] private List<BlockWallAndVisual> listBlock = new List<BlockWallAndVisual>(); 
    [SerializeField] private int stageIndex;

    private Dictionary<ColorType, int> colorToLayerPass = new Dictionary<ColorType, int>();
    public void OnInit(List<Character> listCharacter)
    {
        if(listBlock == null)
        {
          Debug.Log("No Door"); 
          return;   
        } 
        foreach(Character ch in listCharacter)
        {
            foreach(var item in listBlock )
            {
                Physics.IgnoreCollision(ch.characterCollider , item.blockCollier , true); 
                item.door?.SetColor(ColorType.Stair);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Character ch = CacheComponent<Collider,Character>.Get(other);
        if(ch == null) return;

        ch.ReachNewStage(LevelManager.Instance.GetStage(stageIndex));
    
    }

    private void OnTriggerExit(Collider other)
    {
        Character ch = CacheComponent<Collider,Character>.Get(other);
        if(ch == null) return;

        foreach(var item in listBlock)
        {
            Physics.IgnoreCollision(other, item.blockCollier, false);
        }
    }
}
