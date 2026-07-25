using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Debug.Log("death");
            Character player = GameManager.Instance.GetCharacter(other); 
            UIManager.Instance.OpenUI<CanvasFail>(); 
        }   
    }
}