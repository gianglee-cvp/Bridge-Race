using UnityEngine;
using UnityEngine.AI;

public interface IEnemyState
{
    void OnEnter(EnemyAI enemy);
    void OnExecute(EnemyAI enemy);
    void OnExit(EnemyAI enemy);
}
public class EnemyAI : Character
{
    private IEnemyState currentState;
    [SerializeField] public NavMeshAgent agent;
    public bool isGoingToDes = false;
    public Vector2? curDestination;
    protected float timer;
    protected float randomTime;
    protected static IEnemyState Idle = new IdleState();
    protected static IEnemyState Patrol = new PatrolState();
    protected static IEnemyState Build = new BuildState();

    public override void OnInit(Vector3 pos)
    {
        base.OnInit(pos);
        EnableAgent();
        StopAgent();
        currentState = null;
    }
    public override void OnPlay()
    {
        base.OnPlay();
        // agent.enabled = true;
        ChangeState(Patrol);
    }
    private void Update()
    {
        if (isGoingToDes && curDestination != null)
        {
            if(Vector2.Distance(curDestination.Value , GetVector2XZ(transform.position)) < 0.1)
            {
                StopAgent();    
            }
        }
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void EnableAgent()
    {
        agent.enabled = true;
    }
    public void SetAgentDestination(Vector3 destination)
    {
        curDestination = GetVector2XZ(destination);
        agent.SetDestination(destination);
        isGoingToDes = true;
    }
    public void StopAgent()
    {
        agent.SetDestination(transform.position);
        agent.velocity = Vector3.zero;
        isGoingToDes = false;
        curDestination = null;
    }
    public void DisableAgent()
    {
        agent.enabled = false;
    }
    public void ChangeState(IEnemyState newState)
    {
        Debug.Log("EnemyAI: Changing state from " + (currentState != null ? currentState.GetType().Name : "null") + " to " + (newState != null ? newState.GetType().Name : "null"));
        currentState?.OnExit(this);

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public Bridge ChooseStrategy()
    {
        float random = Random.Range(0f,2f); 
        if(random < 1f)
        {
            return currentStage.GetStairLeastOpponent(colorCharacter); 
        }
        else
        {
            return currentStage.GetStairMostPoint(colorCharacter); 
        }
    }
    public override void ReachNewStage(Stage newStage)
    {
        base.ReachNewStage(newStage);
        ChangeState(Patrol); 
    }
    
    public override void ReachLastStep(Step st)
    {
        base.ReachLastStep(st);
        Vector3 target = transform.position + transform.forward * 4f ;
        SetAgentDestination(target); 
    }
    public override void OnFinishLevel()
    {   
        base.OnFinishLevel();
        ChangeState(Idle); 
    }
    public Vector2 GetVector2XZ(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
    public void InitPatrolState()
    {
        StopAgent();
        timer = 0f;
        randomTime = Random.Range(5f, 18f); 
        ChangeAnim(AnimatorTrigger.RUN);
        CanMoveUp = true; 
    }
    public void SetPatrolState()
    {
        ChangeState(Patrol);
    }
    public void ExecutePatrol()
    {
        if (currentStage == null)
        {
            return;
        }

        if (timer >= randomTime ||currentStage.CountActiveBricks(colorCharacter) == 0)
        {
            ChangeState(Build);
            timer = 0f;
            return;
        }
        if(!isGoingToDes)
        {
            Vector3 des = currentStage.GetActiveBrick(colorCharacter);  
            SetAgentDestination(des);
        }
        timer += Time.deltaTime;

    }
}
