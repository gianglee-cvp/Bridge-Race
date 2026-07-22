using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public interface IEnemyState
{
    void OnEnter(EnemyAI enemy);
    void OnExecute(EnemyAI enemy);
    void OnExit(EnemyAI enemy);
}
public class EnemyAI : Character
{
    [SerializeField] private IEnemyState currentState;
 //   public Camera mainCamera; // Test thoi sau delete
    [SerializeField] public NavMeshAgent agent;


    public override void OnInit(Vector3 pos)
    {
        base.OnInit(pos);
        agent.enabled = false;
    }
    public override void OnPlay()
    {
        base.OnPlay();
        agent.enabled = true;
        ChangeState(new PatrolState());
    }
    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public override bool CheckCharacterGoUpStair()
    {
        if (currentState is BuildState)
        {
            return true;
        }

        if(transform.forward.z < 0)
        {
            return false;
        }
        return true;
    }
    public void SetAgentDestination(Vector3 destination)
    {
        if(agent == null)
        {
            Debug.LogError("NavMeshAgent component is not assigned.");
        }
        agent.SetDestination(destination);
    }
    public void ChangeState(IEnemyState newState)
    {
        Debug.Log("EnemyAI: Changing state from " + (currentState != null ? currentState.GetType().Name : "null") + " to " + (newState != null ? newState.GetType().Name : "null"));
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public Stair ChooseStrategy()
    {
        float random = Random.Range(0,2); 
        if(random < 1)
        {
            return GetStairLeastOpponent(); 
        }
        else
        {
            return GetStairMostPoint(colorCharacter); 
        }
    }
    public Stair GetStairLeastOpponent()
    {
        Stair choosenStair = null;
        int st = int.MaxValue;

        foreach (Stair stair in currentStage.listStair)
        {
            int opponentCount = stair.GetOpponentCount(this.colorCharacter);
            if (opponentCount < st)
            {
                st = opponentCount;
                choosenStair = stair;
            }
        }

        return choosenStair;
    }
    public Stair GetStairMostPoint(ENUM_COLOR color)
    {
        Stair mostPointStair = null;
        int mostPointCount = -1;

        foreach (Stair stair in currentStage.listStair)
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
    public override void ReachNewStage(Stage newStage)
    {
        base.ReachNewStage(newStage);
        Debug.Log("Change 2");
        ChangeState(new PatrolState()); 
    }
    
    public override void ReachLastStep(Step st)
    {
        base.ReachLastStep(st);
        Debug.Log("Enemy Reach Last Step");
        Vector3 target = transform.position + transform.forward * 4f ;
        SetAgentDestination(target); 
    }
    public override void OnFinishLevel()
    {   
        Debug.Log("Enemy Finish Level");
        agent.enabled = false;
        base.OnFinishLevel();
        ChangeState(new IdleState()); // Stop the enemy from moving or performing any actions
    }
}