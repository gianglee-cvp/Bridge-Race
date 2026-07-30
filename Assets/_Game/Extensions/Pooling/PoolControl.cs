using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolAmounts;
    public void OnInit()
    {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool/");
        for(int i =0 ; i < poolAmounts.Length ; i++)
        {
            SimplePool.Preload(poolAmounts[i].prefab , poolAmounts[i].amount , poolAmounts[i].parent);
            Debug.Log("Preload " + poolAmounts[i].prefab.name + " Amount: " + poolAmounts[i].amount);   
        }
    }
}
[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab; 
    public int amount; 
    public Transform parent;

}
public enum PoolType
{
    Brick = 0, 
    CharacterBrick = 1,
    Level = 2 
}
