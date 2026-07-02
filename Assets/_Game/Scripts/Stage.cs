using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Stage : MonoBehaviour
{
    private Queue<Brick> brickToRemain = new Queue<Brick>();
    private List<Brick> activeBricks = new List<Brick>();
    private Dictionary<ENUM_COLOR, List<Brick>> colorBricks = new Dictionary<ENUM_COLOR, List<Brick>>();
    public int stageIndex; // cho vao dictionary hoac gan khi spawn
    [SerializeField] public List<Stair> listStair = new List<Stair>();
    public Dictionary<ENUM_COLOR, bool > isCoLorSpawned = new Dictionary<ENUM_COLOR, bool>();
    public void OnRemainBrick()
    {
//        Debug.Log("Stage: " + gameObject.name + " OnRemainBrick");
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

    public void AddActiveBrick(Brick brick , ENUM_COLOR color)
    {
        activeBricks.Add(brick);
        if (!colorBricks.ContainsKey(color))
        {
            colorBricks[color] = new List<Brick>();
        }
        colorBricks[color].Add(brick);
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
    public void CloseAllDoor(ENUM_COLOR color)
    {
        foreach(Stair st in listStair)
        {
            st.CloseDoor(color); 
        }
    }
    public void SpawnBrick(ENUM_COLOR color)
    {

        if(isCoLorSpawned.ContainsKey(color) && isCoLorSpawned[color]) return ; 
        isCoLorSpawned[color] = true;

        if(colorBricks.ContainsKey(color))
        {
            List<Brick> bricksOfColor = colorBricks[color];
            foreach (Brick brick in bricksOfColor)
            {
                brick.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("No bricks found for color: " + color);    
        }
    }
}
    