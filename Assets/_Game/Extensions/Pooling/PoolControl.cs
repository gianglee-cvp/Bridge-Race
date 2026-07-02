using Unity.VisualScripting;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] PoolAmount[] poolAmounts;
    void Awake()
    {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool/");

        for (int i = 0; i < gameUnits.Length; i++)
        {
            SimplePool.Preload(gameUnits[i], 0 , new GameObject(gameUnits[i].name).transform);
        }
        for(int i =0 ; i < poolAmounts.Length ; i++)
        {
            SimplePool.Preload(poolAmounts[i].prefab , poolAmounts[i].amount , poolAmounts[i].parent);
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
    Bullet_3 = 2 
}
