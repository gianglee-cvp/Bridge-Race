using UnityEngine;
using System.Collections.Generic;
public enum AnimatorTrigger
{
    IDLE = 0,
    RUN = 1,
    FALL = 2,
    WIN = 3,
    LOSE = 4,
    Dance = 5
}

public class Character : MonoBehaviour,IColor
{
    [SerializeField] public Transform parentBrick;
    [SerializeField] public ColorType colorCharacter;
    [SerializeField] public Collider characterCollider;
    [SerializeField] protected Transform rotatePart;
    [SerializeField] private Animator animator;
    [SerializeField] private Renderer colorPart;
    public List<CharacterBrick> listBricks = new List<CharacterBrick>();
    public Stage currentStage;
    private AnimatorTrigger currentAnim ;
    private int point; 
    public int Point { get => point;}
    public bool canMoveUp = true;
    public bool CanMoveUp
    {
        get => canMoveUp;
        set => canMoveUp = value;
    }
    public Vector3 BlockedStepForward { get; set; }
    public int CurrentBrickCount => listBricks.Count;
    public virtual void OnInit(Vector3 pos)
    {
        gameObject.SetActive(true); 
        transform.position = pos;
        transform.rotation = Quaternion.identity;
        canMoveUp = true; 
        BlockedStepForward = Vector3.zero;
        characterCollider.enabled = false;
        ChangeAnim(AnimatorTrigger.Dance);
        Debug.Log(gameObject.name+ " name");
    }
    public virtual void OnPlay()
    {
        characterCollider.enabled = true;
        ChangeAnim(AnimatorTrigger.IDLE);
        OnChangeStage(LevelManager.Instance.GetStage(0));
        point = 0 ; 
    }

    public virtual void AddBrick(Brick brick)
    {
        Vector3 spawnWorldPos = brick.transform.position;
        Quaternion spawnWorldRot = brick.transform.rotation;

        CharacterBrick chBrick = SimplePool.Spawn<CharacterBrick>(
            PoolType.CharacterBrick, 
            spawnWorldPos, 
            spawnWorldRot, 
            null
        );

        chBrick.OnCollect(brick.colorBrick, this, spawnWorldPos, spawnWorldRot);
        listBricks.Add(chBrick);

        point ++;
    }
    public void RemoveBrick()
    {
        if (listBricks.Count > 0)
        {
            int index = listBricks.Count - 1;
            CharacterBrick chBrick = listBricks[index]; 
            SimplePool.DeSpawn(chBrick); 
            listBricks.RemoveAt(index); 
            
        }
    }
    public void ClearAllBrick()
    {
        foreach (var brick in listBricks)
        {
            SimplePool.poolInstance[PoolType.CharacterBrick].DeSpawn(brick);
        }
        listBricks.Clear();
    }
    public virtual bool CheckCharacterGoUpStair(Vector3 stepForward)
    {
        if(Vector3.Dot(rotatePart.forward , stepForward) > 0f)
        {
            return true;
        }
        return false;
    }
    public virtual void OnBlockedByStep(Vector3 stepForward)
    {
    }
    public void SetColor(ColorType color)
    {
        ((IColor)this).ISetColor(colorPart,ref colorCharacter , color); 
    }

    public virtual void ReachNewStage(Stage newStage)
    {
        if(currentStage == newStage) return ;
        OnChangeStage(newStage);
    }
    public virtual void OnChangeStage(Stage newStage)
    {
        if(currentStage == null || currentStage != newStage)
        {
            currentStage = newStage;
            newStage.SpawnBrick(colorCharacter);
        }
    }
    public virtual void ReachLastStep(Step st)
    {
    }
    public void ChangeAnim(AnimatorTrigger anim)
    {
        if(currentAnim != anim)
        {
            animator.ResetTrigger(
                Constants.listTriggerAnimator[(int)currentAnim]);
            currentAnim = anim;
            animator.SetTrigger(
                Constants.listTriggerAnimator[(int)currentAnim]);
        }
    }

    public virtual void OnFinishLevel()
    {
    }   
    public virtual void OnWin(int seed)
    {
        ClearAllBrick(); 
        OnFinishLevel();
        LevelManager.Instance.PlaceAtWinPosition(this, seed);

        rotatePart.localRotation = Quaternion.Euler(Vector3.zero); 
        ChangeAnim(AnimatorTrigger.WIN);
    }
    public virtual void OnLose()
    {
        OnFinishLevel(); 
    }

    public virtual void OnExitGame()
    {
        ClearAllBrick();
        currentStage = null; 
        characterCollider.enabled = false;
        gameObject.SetActive(false); 
    }
}
