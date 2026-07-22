using UnityEngine;
using System.Collections.Generic;
public class Stage : MonoBehaviour
{
    private Dictionary<ENUM_COLOR, Queue<Brick>> brickToRemain_ver2 = new Dictionary<ENUM_COLOR, Queue<Brick>>(); 
    // private List<Brick> activeBricks = new List<Brick>();
    private Dictionary<ENUM_COLOR, List<Brick>> activeBricks_ver2 = new Dictionary<ENUM_COLOR, List<Brick>>() ; 
    public int stageIndex; // cho vao dictionary hoac gan khi spawn
    [SerializeField] public List<Stair> listStair = new List<Stair>();
    public void OnRemainBrick(ENUM_COLOR color)
    {
        if(!brickToRemain_ver2.ContainsKey(color) || brickToRemain_ver2[color].Count == 0)
        {
            return;
        }
        Brick br = brickToRemain_ver2[color].Dequeue();
        if (!activeBricks_ver2.ContainsKey(color))
        {
            activeBricks_ver2[color] = new List<Brick>(); 
        }
        activeBricks_ver2[color].Add(br); 
        br.OnRemain(color);  
    }
    public void AddBrickToRemain(Brick brick)
    {
        if (!brickToRemain_ver2.ContainsKey(brick.colorBrick))
        {
            brickToRemain_ver2[brick.colorBrick] = new Queue<Brick>();
        }
        brickToRemain_ver2[brick.colorBrick].Enqueue(brick);
        brick.OnCollect(); 
    }

    // dung khi spawn luc dau (spawn ra luc dau la mau none nen la can truyen color)
    public void AddBrickToRemain(Brick brick , ENUM_COLOR color)
    {
        if (!brickToRemain_ver2.ContainsKey(color))
        {
            brickToRemain_ver2[color] = new Queue<Brick>(); 
        }
        
        brickToRemain_ver2[color].Enqueue(brick); 
        brick.OnCollect();  
    }
    public int CountActiveBricks(ENUM_COLOR color) 
    {
        if(activeBricks_ver2.ContainsKey(color))
        {
            return activeBricks_ver2[color].Count;
        }
        else
        {
            Debug.LogError("count error"); 
            return 0;
        }
    }
    
    public Transform GetActiveBrick(ENUM_COLOR color)
    {
        int cnt = activeBricks_ver2[color].Count;
        if (cnt == 0)
        {
            Debug.LogError("No active bricks found for color: " + color);
        }
        int randomIndex = Random.Range(0, cnt);
        return activeBricks_ver2[color][randomIndex].transform;
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
        if(brickToRemain_ver2.ContainsKey(color) && brickToRemain_ver2[color].Count > 0)
        {
            while(brickToRemain_ver2[color].Count > 0)
            {
                Brick br = brickToRemain_ver2[color].Dequeue(); 
                br.OnRemain(color);

                if (!activeBricks_ver2.ContainsKey(color))
                {
                    activeBricks_ver2[color] = new List<Brick>() ;
                }
                activeBricks_ver2[color].Add(br); 
            }
        }
    }
    public void OnEnd()
    {      
        foreach( var q in brickToRemain_ver2.Values)
        {
            while(q.Count > 0)
            {
                Brick br = q.Dequeue(); 
                SimplePool.DesSpawn(br); 
            }
        }


        foreach(var listBrick in activeBricks_ver2.Values)
        {
            foreach(var br in listBrick)
            {
                SimplePool.DesSpawn(br); 
            }
        }
        activeBricks_ver2.Clear();


        foreach( var stair in listStair)
        {
            stair.OnEnd(); 
        }
    }
}
    