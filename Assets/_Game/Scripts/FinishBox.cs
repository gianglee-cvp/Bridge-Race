using UnityEngine;

public class FinishBox : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.CharacterTag))
        {
            Character ch = LevelManager.Instance.GetCharacter(other);
            GameManager.Instance.OnCharacterWin(ch);
        }
    }

}   
