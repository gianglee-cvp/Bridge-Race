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
            foreach(var col in blockCollider)
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
            foreach(var col in blockCollider)
            {
                Physics.IgnoreCollision(other , col , false); 
            }

            Character character = LevelManager.Instance.GetCharacter(other);
            character.ReachNewStage(LevelManager.Instance.GetStage(stageIndex));
            Debug.Log("2");
        }
    }
}
