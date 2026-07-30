using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //TODO phan biet enemy va player 
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CharacterTag))
        {
            UIManager.Instance.OpenUI<CanvasFail>(); 
        }   
    }
}