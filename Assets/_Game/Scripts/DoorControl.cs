using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private int stageIndex; 
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
        {
            Character ch = GameManager.Instance.GetCharacter(other);
            ch.ReachNewStage(GameManager.Instance.stageList[stageIndex]); 
        }
    }
}