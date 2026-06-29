using UnityEngine;
using System.Collections.Generic; 
public class Stage : MonoBehaviour
{
    private Queue<Brick> brickToRemain = new Queue<Brick>();
    private List<Brick> activeBricks = new List<Brick>();
    [SerializeField] public List<Stair> listStair = new List<Stair>();



    public void OnRemainBrick()
    {
        Debug.Log("Stage: " + gameObject.name + " OnRemainBrick");
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
    public void RemoveActiveBrick(Brick brick)
    {
        activeBricks.Remove(brick);
    }
    public int CountActiveBricks()
    {
        return activeBricks.Count;
    }
    
    public Transform GetActiveBrick(ENUM_COLOR color)
    {
        List<Brick> filteBrickByColor = activeBricks.FindAll(brick => brick.colorBrick == color);
        if (filteBrickByColor.Count == 0)
        {
            Debug.LogError("No active bricks found for color: " + color);
        }
        int randomIndex = Random.Range(0, filteBrickByColor.Count);
        return filteBrickByColor[randomIndex].transform;
    }
}
    