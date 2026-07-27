using UnityEngine;

public class FinishBox : MonoBehaviour
{
    [SerializeField] private Transform firstSeed; 
    [SerializeField] private Transform secondSeed;
    [SerializeField] private Transform thirdSeed;
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.CharacterTag))
        {
            Character ch = GameManager.Instance.GetCharacter(other);
            GameManager.Instance.OnCharacterWin(ch, firstSeed, secondSeed, thirdSeed);
        }
    }

}   