using UnityEngine;
using System.Collections.Generic;
using System;
public enum ENUM_ANIMATOR_TRIGGER
{
    IDLE = 0,
    RUN = 1,
    FALL = 2,
    WIN = 3,
    LOSE = 4
}

public class Character : MonoBehaviour
{
    [SerializeField] public Transform parentBrick;
    [SerializeField] public ENUM_COLOR colorCharacter;
    [SerializeField] public Collider characterCollider;
    [SerializeField] public Transform rotatePart;
    [SerializeField] private Animator animator;
    [SerializeField] public CharacterBrick chBrickPrefab; // TODO dduaw vafo gamemanager de load 
    public List<CharacterBrick> listBricks = new List<CharacterBrick>();
    public Stage currentStage;
    private ENUM_ANIMATOR_TRIGGER currentAnim ;
    private int point; 
    public int Point { get => point;}


    // public int currentStageIndex = 0;
    public int currentBrickCount = 0 ; 
    public virtual void OnInit()    
    {
        SetAnim(ENUM_ANIMATOR_TRIGGER.IDLE);
        // currentStage = GameManager.Instance.stageList[0];
        OnChangeStage(GameManager.Instance.stageList[0]);
        point = 0 ; 
    }
    // void OnDespawn()
    // {
        
    // }  

    public void AddBrick(Brick brick)
    {
        CharacterBrick chBrick = Instantiate(chBrickPrefab, parentBrick , false);
        listBricks.Add(chBrick);
        brick.OnCollect(); 
        //TODO : cho vao oncollect cua brick
        chBrick.transform.localPosition = new Vector3(0, (currentBrickCount) * 0.15f, 0);
        chBrick.transform.localRotation = Quaternion.identity;
        chBrick.OnCollect(brick.colorBrick, this);
        
        currentBrickCount++;
        point ++;
    }
    public void ClearAllBrick()
    {
        foreach (var brick in listBricks)
        {
            Destroy(brick.gameObject); // TODO : cho vao pool
        }
        listBricks.Clear();
        currentBrickCount = 0;
    }
    public virtual bool CheckCharacterGoUpStair()
    {
        return true;
    }
    public bool CheckDistanceToStep(Transform stepTf)
    {
        Vector2 cr = new Vector2(transform.position.x, transform.position.z);
        Vector2 st = new Vector2(stepTf.position.x, stepTf.position.z);
        float distance = Vector2.Distance(cr, st);
        // Debug.Log("CheckDistanceToStep: " + stepTf.gameObject.name + " distance: " + distance);
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
            int index = listBricks.Count - 1;

            listBricks[index].gameObject.SetActive(false); // TODO : cho vào pool
            listBricks.RemoveAt(index); 
            
            currentBrickCount--;
            
        }
    }
    public virtual void ReachNewStage(Stage newStage)
    {
        if(currentStage == newStage) return ;
        currentStage.CloseAllDoor(colorCharacter); 
        //currentStage = newStage; 
        OnChangeStage(newStage);
    }
    public virtual void ReachLastStep(Step st)
    {
        Debug.Log("Characte Reach Last Step"); 
        st.SetStopPointOnLastStep(this);
    }
    public void SetAnim(ENUM_ANIMATOR_TRIGGER anim)
    {
        if(currentAnim != anim)
        {
            animator.ResetTrigger(
                GameManager.Instance.listTriggerAnimator[(int)currentAnim]);
            currentAnim = anim;
            animator.SetTrigger(
                GameManager.Instance.listTriggerAnimator[(int)currentAnim]);
        }
    }

    public virtual void OnFinishLevel()
    {
        ClearAllBrick();
    }   
    public virtual void OnWin(Transform Seed)
    {
        OnFinishLevel();
        transform.SetPositionAndRotation(Seed.position, Seed.rotation);
        SetAnim(ENUM_ANIMATOR_TRIGGER.WIN);
    }
    public virtual void OnChangeStage(Stage newStage)
    {
        if(currentStage != newStage)
        {
            currentStage = newStage;
            newStage.SpawnBrick(colorCharacter);
            Debug.Log("Character: " + gameObject.name + " Change Stage to: " + newStage.gameObject.name);
        }
    }
}
