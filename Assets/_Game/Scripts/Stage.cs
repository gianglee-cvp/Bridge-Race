using UnityEngine;
using System.Collections.Generic; 
public class Stage : MonoBehaviour
{
    private Queue<Brick> brickToRemain = new Queue<Brick>();
    private List<Brick> activeBricks = new List<Brick>();
    [SerializeField] public List<Stair> listStair = new List<Stair>();



    public void OnRemainBrick()
    {
        if(brickToRemain.Count == 0)
        {
            return;
        }
        Brick br = brickToRemain.Dequeue();
        br.gameObject.SetActive(true);
    }

    public void AddBrickToRemain(Brick brick)
    {
        brickToRemain.Enqueue(brick);
    }

    public void AddActiveBrick(Brick brick)
    {
        activeBricks.Add(brick);
    }

    public int CountActiveBricks()
    {
        return activeBricks.Count;
    }
    
    public Transform GetActiveBrick()
    {
        int randomIndex = Random.Range(0, activeBricks.Count);
        return activeBricks[randomIndex].transform;
    }
}
    