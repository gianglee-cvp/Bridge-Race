using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Renderer colorPart;
    public List<CharacterBrick> listBricks = new List<CharacterBrick>();
    public Stage currentStage;
    private ENUM_ANIMATOR_TRIGGER currentAnim ;
    private int point; 
    public int Point { get => point;}


    // public int currentStageIndex = 0;
    public int currentBrickCount = 0 ; 
    public virtual void OnInit(Vector3 pos)
    {
        gameObject.SetActive(true); 
        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }
    public virtual void OnPlay()
    {
        SetAnim(ENUM_ANIMATOR_TRIGGER.IDLE);
        OnChangeStage(GameManager.Instance.stageList[0]);
        point = 0 ; 
    }

    public void AddBrick(Brick brick)
    {
        Vector3 spawnWorldPos = brick.transform.position;
        Quaternion spawnWorldRot = brick.transform.rotation;

        CharacterBrick chBrick = SimplePool.Spawn<CharacterBrick>(
            chBrickPrefab.poolType, 
            spawnWorldPos, 
            spawnWorldRot, 
            null
        );
        
        listBricks.Add(chBrick);
   
        chBrick.OnCollect(brick.colorBrick, this, spawnWorldPos, spawnWorldRot);
        brick.OnCollect(); 

        currentBrickCount++;
        point ++;
    }
    public void ClearAllBrick()
    {
        foreach (var brick in listBricks)
        {
            //Destroy(brick.gameObject); // TODO : cho vao pool
            SimplePool.poolInstance[PoolType.CharacterBrick].DesSpawn(brick);
        }
        // SimplePool.poolInstance[PoolType.CharacterBrick].Collect(); 
        listBricks.Clear();
        currentBrickCount = 0;
    }
    public virtual bool CheckCharacterGoUpStair()
    {
        return false;
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
    public void SetColor(ENUM_COLOR color)
    {
        colorPart.material = GameManager.Instance.colorDataSO.GetMaterial(color);
        colorCharacter = color;
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
        if(currentStage == null || currentStage != newStage)
        {
            currentStage = newStage;
            newStage.SpawnBrick(colorCharacter);
        }
    }
    public virtual void OnExitGame()
    {
        ClearAllBrick();
        currentStage = null; 
        gameObject.SetActive(false); 
    } 
}
