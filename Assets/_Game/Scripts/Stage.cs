using UnityEngine;
using System.Collections.Generic;

public class BrickBucket
{
    public Queue<Brick> Inactive = new Queue<Brick>();
    public List<Brick> Active = new List<Brick>();
}

public class Stage : MonoBehaviour
{
    private Dictionary<ColorType, BrickBucket> bricksByColor = new Dictionary<ColorType, BrickBucket>();
    public int stageIndex; 
    [SerializeField] public List<Bridge> listStair = new List<Bridge>();
    [SerializeField] private DoorControl door ; 

    private BrickBucket GetBucket(ColorType color)
    {
        if (!bricksByColor.TryGetValue(color, out BrickBucket bucket))
        {
            bucket = new BrickBucket();
            bricksByColor[color] = bucket;
        }

        return bucket;
    }

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
        if (!bricksByColor.TryGetValue(color, out BrickBucket bucket) || bucket.Inactive.Count == 0)
        {
            return;
        }

        Brick br = bucket.Inactive.Dequeue();
        bucket.Active.Add(br); 
        br.OnRemain(color);  
    }

    public void AddBrickToRemain(Brick brick)
    {
        BrickBucket bucket = GetBucket(brick.colorBrick);
        bucket.Active.Remove(brick);
        bucket.Inactive.Enqueue(brick);
        brick.OnCollect(); 
    }

    public void AddBrickToRemain(Brick brick , ColorType color)
    {
        BrickBucket bucket = GetBucket(color);
        bucket.Inactive.Enqueue(brick); 
        brick.OnCollect();  
    }

    public int CountActiveBricks(ColorType color) 
    {
        if (bricksByColor.TryGetValue(color, out BrickBucket bucket))
        {
            return bucket.Active.Count;
        }

        Debug.LogError("count error"); 
        return 0;
    }
    
    public Vector3 GetActiveBrick(ColorType color)
    {
        BrickBucket bucket = bricksByColor[color];
        int cnt = bucket.Active.Count;
        int randomIndex = Random.Range(0, cnt);
        return bucket.Active[randomIndex].transform.position;
    }

    public void SpawnBrick(ColorType color)
    {
        if (bricksByColor.TryGetValue(color, out BrickBucket bucket) && bucket.Inactive.Count > 0)
        {
            while (bucket.Inactive.Count > 0)
            {
                Brick br = bucket.Inactive.Dequeue(); 
                br.OnRemain(color);
                bucket.Active.Add(br); 
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
        foreach (var bucket in bricksByColor.Values)
        {
            while (bucket.Inactive.Count > 0)
            {
                Brick br = bucket.Inactive.Dequeue(); 
                ReturnBrickToPool(br); 
            }

            foreach (var br in bucket.Active)
            {
                ReturnBrickToPool(br); 
            }
        }

        bricksByColor.Clear();

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
    
