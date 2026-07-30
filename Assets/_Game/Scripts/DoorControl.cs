using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Collider[] blockColider; 
    [SerializeField] private int stageIndex;

    private Dictionary<ENUM_COLOR, int> colorToLayerPass = new Dictionary<ENUM_COLOR, int>();
    public void OnInit(List<Character> listCharacter)
    {
        if(blockColider == null)
        {
          Debug.Log("No Door"); 
          return;   
        } 
        foreach(Character ch in listCharacter)
        {
            foreach(var col in blockColider)
            {
                Physics.IgnoreCollision(ch.characterCollider , col , true); 
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CharacterTag))
        {
            //Physics.IgnoreCollision(other , blockColider , false); 
            foreach(var col in blockColider)
            {
                Physics.IgnoreCollision(other , col , false); 
            }

            Character character = LevelManager.Instance.GetCharacter(other);
            character.ReachNewStage(LevelManager.Instance.GetStage(stageIndex));
            Debug.Log("2");
        }
    }
}
