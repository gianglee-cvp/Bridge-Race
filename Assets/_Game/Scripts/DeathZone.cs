using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //TODO phan biet enemy va player 
    public void OnTriggerEnter(Collider other)
    {
        Character character = CacheComponent<Collider,Character>.Get(other);
        if(character == null) return;
        if(character is Player)
        {
            GameManager.Instance.ChangeState((int)GameStateType.Lose);
        }
    }
}