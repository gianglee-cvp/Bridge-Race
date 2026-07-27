using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            UIManager.Instance.OpenUI<CanvasFail>(); 
        }   
    }
}