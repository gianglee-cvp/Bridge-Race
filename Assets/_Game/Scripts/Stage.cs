using UnityEngine;
using System.Collections.Generic;
public class Stage : MonoBehaviour
{
    private Dictionary<ColorType, Queue<Brick>> inactive = new Dictionary<ColorType, Queue<Brick>>(); 
    private Dictionary<ColorType, List<Brick>> active = new Dictionary<ColorType, List<Brick>>() ; 
    public int stageIndex; 
    [SerializeField] public List<Bridge> listStair = new List<Bridge>();
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
        if(door != null)
        {
            door.OnInit(LevelManager.Instance.Characters); 
        }
    }

    public void OnRemainBrick(ColorType color)
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
    public void AddBrickToRemain(Brick brick , ColorType color)
    {
        if (!inactive.ContainsKey(color))
        {
            inactive[color] = new Queue<Brick>(); 
        }
        
        inactive[color].Enqueue(brick); 
        brick.OnCollect();  
    }
    public int CountActiveBricks(ColorType color) 
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
    
    public Vector3 GetActiveBrick(ColorType color)
    {
        int cnt = active[color].Count;
        int randomIndex = Random.Range(0, cnt);
        return active[color][randomIndex].transform.position;
    }
    public void SpawnBrick(ColorType color)
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
    public Bridge GetStairMostPoint(ColorType color)
    {
        Bridge mostPointStair = null;
        int mostPointCount = -1;

        foreach (Bridge stair in this.listStair)
        {
            int pointCount = stair.GetMaxPointCount(color);
            if (pointCount > mostPointCount)
            {
                mostPointCount = pointCount;
                mostPointStair = stair;
            }
        }

        return mostPointStair;
    }
    public Bridge GetStairLeastOpponent(ColorType color)
    {
        Bridge chosenStair = null;
        int st = int.MaxValue;

        foreach (Bridge stair in this.listStair)
        {
            int opponentCount = stair.GetOpponentCount(color);
            if (opponentCount < st)
            {
                st = opponentCount;
                chosenStair = stair;
            }
        }

        return chosenStair;
    }
    public void OnEnd()
    {      
        foreach( var q in inactive.Values)
        {
            while(q.Count > 0)
            {
                Brick br = q.Dequeue(); 
                ReturnBrickToPool(br); 
            }
        }

        foreach(var listBrick in active.Values)
        {
            foreach(var br in listBrick)
            {
                ReturnBrickToPool(br); 
            }
        }

        active.Clear();
        inactive.Clear();

        foreach( var stair in listStair)
        {
            stair.OnEnd(); 
        }
    }

    private void ReturnBrickToPool(Brick brick)
    {
        if (brick == null)
        {
            return;
        }
        SimplePool.DeSpawn(brick);
        brick.transform.SetParent(null);
    }
}
    
