using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Stage : MonoBehaviour
{
    private Dictionary<ENUM_COLOR, Queue<Brick>> brickToRemain = new Dictionary<ENUM_COLOR, Queue<Brick>>();
    // private List<Brick> activeBricks = new List<Brick>();
    private Dictionary<ENUM_COLOR, List<Brick>> activeBricks = new Dictionary<ENUM_COLOR, List<Brick>>();
    private Dictionary<ENUM_COLOR, List<Brick>> colorBricks = new Dictionary<ENUM_COLOR, List<Brick>>();
    public int stageIndex; // cho vao dictionary hoac gan khi spawn
    [SerializeField] public List<Stair> listStair = new List<Stair>();
    public Dictionary<ENUM_COLOR, bool > isCoLorSpawned = new Dictionary<ENUM_COLOR, bool>();
    public void OnRemainBrick(ENUM_COLOR color)
    {
        if(!brickToRemain.ContainsKey(color) || brickToRemain[color].Count == 0)
        {
            return;
        }
        Brick br = brickToRemain[color].Dequeue();
        AddActiveBrick(br, br.colorBrick);  
        br.gameObject.SetActive(true);
    }
    public void AddBrickToRemain(Brick brick)
    {
        if (!brickToRemain.ContainsKey(brick.colorBrick))
        {
            brickToRemain[brick.colorBrick] = new Queue<Brick>();
        }
        brickToRemain[brick.colorBrick].Enqueue(brick);
    }

    public void AddActiveBrick(Brick brick , ENUM_COLOR color)
    {

        if (!activeBricks.ContainsKey(color))
        {
            activeBricks[color] = new List<Brick>();
        }
        activeBricks[color].Add(brick);

        if (!colorBricks.ContainsKey(color))
        {
            colorBricks[color] = new List<Brick>();
        }
        colorBricks[color].Add(brick);
    }
    public void RemoveActiveBrick(Brick brick)
    {
        foreach (var kvp in activeBricks)
        {
            kvp.Value.Remove(brick);
        }
    }
    public int CountActiveBricks(ENUM_COLOR color) 
    {
        if(activeBricks.ContainsKey(color))
        {
            return activeBricks[color].Count;
        }
        else
        {
            Debug.LogError("count error"); 
            return 0;
        }
    }
    
    public Transform GetActiveBrick(ENUM_COLOR color)
    {
        int cnt = activeBricks[color].Count;
        if (cnt == 0)
        {
            Debug.LogError("No active bricks found for color: " + color);
        }
        int randomIndex = Random.Range(0, cnt);
        return activeBricks[color][randomIndex].transform;
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
    }
    public void OnEnd()
    {
        foreach (var kvp in colorBricks)
        {
            foreach (var brick in kvp.Value)
            {
                if (!brick.gameObject.activeSelf)
                {
                    brick.gameObject.SetActive(true);
                }
                SimplePool.DesSpawn(brick);
            }
        }
        
        brickToRemain.Clear();
        activeBricks.Clear();
        colorBricks.Clear();
        isCoLorSpawned.Clear();
        Debug.Log("Return color1");
        foreach( var stair in listStair)
        {
            Debug.Log("Return color 2");
            stair.OnEnd(); 
        }
    }
}
    