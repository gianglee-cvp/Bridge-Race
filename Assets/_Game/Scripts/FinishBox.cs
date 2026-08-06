using UnityEngine;

public class FinishBox : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Character ch = CacheComponent<Collider, Character>.Get(other);
        if(ch == null) return;
        GameManager.Instance.HandleCharacterRank(ch);
    }

}   
