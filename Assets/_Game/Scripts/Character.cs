using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [SerializeField] public Transform parentBrick;
    [SerializeField] public ENUM_COLOR colorCharacter;
    [SerializeField] public Collider characterCollider;
    [SerializeField] public Transform rotatePart;
    public List<Brick> listBricks = new List<Brick>();
    public int currentBrickCount = 0 ; 
    void OnInit()
    {
    }
    // void OnDespawn()
    // {
        
    // }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Brick"))
        {
            if(colorCharacter == GameManager.Instance.GetBrick(other).colorBrick)
            {
                Brick brick = GameManager.Instance.GetBrick(other);
                AddBrick(brick);
                GameManager.Instance.UnregisterBrick(other);
            }
        }
    }    
    public void AddBrick(Brick brick)
    {
        listBricks.Add(brick);
        brick.transform.SetParent(parentBrick, false); 
        brick.transform.localPosition = new Vector3(0, (currentBrickCount-1) * 0.15f, 0);
        brick.transform.localRotation = Quaternion.identity;
        currentBrickCount++;
    }
    public bool CheckCharacterGoUpStair()
    {
        if(rotatePart.forward.z < 0)
        {
            return false;
        }
        return true;
    }
    public bool CheckDistanceToStep(Transform stepTf)
    {
        Vector2 cr = new Vector2(transform.position.x, transform.position.z);
        Vector2 st = new Vector2(stepTf.position.x, stepTf.position.z);
        float distance = Vector2.Distance(cr, st);
        Debug.Log("CheckDistanceToStep: " + stepTf.gameObject.name + " distance: " + distance);
        if(distance < 0.9f) 
        {
            return true;
        }
        return false;
    }

    //TODO cho vào trong pool 
    public void RemoveBrick()
    {
        if (listBricks.Count > 0)
        {
            listBricks[currentBrickCount - 1].gameObject.SetActive(false);
            listBricks.RemoveAt(listBricks.Count - 1);
            currentBrickCount--;
        }
    }
}
