using UnityEngine;
using System.Collections.Generic;
public class Stage : MonoBehaviour
{
    private Dictionary<ENUM_COLOR, Queue<Brick>> inactive = new Dictionary<ENUM_COLOR, Queue<Brick>>(); 
    private Dictionary<ENUM_COLOR, List<Brick>> active = new Dictionary<ENUM_COLOR, List<Brick>>() ; 
    public int stageIndex; 
    [SerializeField] public List<Stair> listStair = new List<Stair>();
    [SerializeField] private DoorControl door ; 

    public void Load(StageData stageData)
    {
        foreach (var brickData in stageData.bricks)
        {
            Brick unit = SimplePool.Spawn<Brick>(
                PoolType.Brick,
                brickData.position,
                transform.rotation,
                transform);

            AddBrickToRemain(unit, brickData.color);
            unit.stage = this;
        }
    }

    public void OnRemainBrick(ENUM_COLOR color)
    {
        if(!inactive.ContainsKey(color) || inactive[color].Count == 0)
        {
            return;
        }
        Brick br = inactive[color].Dequeue();
        if (!active.ContainsKey(color))
        {
            active[color] = new List<Brick>(); 
        }
        active[color].Add(br); 
        br.OnRemain(color);  
    }
    public void AddBrickToRemain(Brick brick)
    {
        if (active.ContainsKey(brick.colorBrick))
        {
            active[brick.colorBrick].Remove(brick);
        }

        if (!inactive.ContainsKey(brick.colorBrick))
        {
            inactive[brick.colorBrick] = new Queue<Brick>();
        }
        inactive[brick.colorBrick].Enqueue(brick);
        brick.OnCollect(); 
    }
    public void AddBrickToRemain(Brick brick , ENUM_COLOR color)
    {
        if (!inactive.ContainsKey(color))
        {
            inactive[color] = new Queue<Brick>(); 
        }
        
        inactive[color].Enqueue(brick); 
        brick.OnCollect();  
    }
    public int CountActiveBricks(ENUM_COLOR color) 
    {
        if(active.ContainsKey(color))
        {
            return active[color].Count;
        }
        else
        {
            Debug.LogError("count error"); 
            return 0;
        }
    }
    
    public Transform GetActiveBrick(ENUM_COLOR color)
    {
        int cnt = active[color].Count;
        int randomIndex = Random.Range(0, cnt);
        return active[color][randomIndex].transform;
    }
    public void SpawnBrick(ENUM_COLOR color)
    {
        if(inactive.ContainsKey(color) && inactive[color].Count > 0)
        {
            while(inactive[color].Count > 0)
            {
                Brick br = inactive[color].Dequeue(); 
                br.OnRemain(color);

                if (!active.ContainsKey(color))
                {
                    active[color] = new List<Brick>() ;
                }
                active[color].Add(br); 
            }
        }
    }
    public void OnEnd()
    {      
        if(door != null)
        {
            door.OnEnd(); 
        }

        foreach( var q in inactive.Values)
        {
            while(q.Count > 0)
            {
                Brick br = q.Dequeue(); 
                SimplePool.DesSpawn(br); 
            }
        }


        foreach(var listBrick in active.Values)
        {
            foreach(var br in listBrick)
            {
                SimplePool.DesSpawn(br); 
            }
        }
        active.Clear();
        inactive.Clear();


        foreach( var stair in listStair)
        {
            stair.OnEnd(); 
        }
    }
}
    
