using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //TODO phan biet enemy va player 
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CharacterTag))
        {
            Character character = LevelManager.Instance.GetCharacter(other);
            if(character is Player)
            {
                GameManager.Instance.ChangeState(new LoseState());
            }
        }   
    }
}