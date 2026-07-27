using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CharacterTag))
        {
            UIManager.Instance.OpenUI<CanvasFail>(); 
        }   
    }
}