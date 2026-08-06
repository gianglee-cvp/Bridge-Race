using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Collider[] blockCollider; 
    [SerializeField] private int stageIndex;

    private Dictionary<ColorType, int> colorToLayerPass = new Dictionary<ColorType, int>();
    public void OnInit(List<Character> listCharacter)
    {
        if(blockCollider == null)
        {
          Debug.Log("No Door"); 
          return;   
        } 
        foreach(Character ch in listCharacter)
        {
            foreach(Collider col in blockCollider)
            {
                Physics.IgnoreCollision(ch.characterCollider , col , true); 
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Character ch = CacheComponent<Collider,Character>.Get(other);
        if(ch == null) return;

        //Physics.IgnoreCollision(other , blockColider , false); 
        foreach(var col in blockCollider)
        {
            Physics.IgnoreCollision(other , col , false); 
        }
        ch.ReachNewStage(LevelManager.Instance.GetStage(stageIndex));
    
    }
}
