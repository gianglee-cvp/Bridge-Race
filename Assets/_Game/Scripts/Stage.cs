using UnityEngine;
using System.Collections.Generic;
public class Stage : MonoBehaviour
{
    Queue<Brick> brickToRemain = new Queue<Brick>();
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
}
    