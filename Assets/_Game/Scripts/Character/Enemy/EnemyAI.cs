using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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


    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new PatrolState());
    }
    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
        // Delete 
        // if(Mouse.current.leftButton.wasPressedThisFrame)
        // {
        //     Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //     RaycastHit hit;
        //     if(Physics.Raycast(ray, out hit))
        //     {
        //         agent.SetDestination(hit.point);
        //         Debug.Log("EnemyAI: Set destination to " + hit.point);
        //     }
        // }

    }
    public override bool CheckCharacterGoUpStair()
    {
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
        int randomIndex = Random.Range(0,2); // dung switch để mở rộng thêm strategy 
        // Debug.Log("EnemyAI: ChooseStrategy called, randomIndex = " + randomIndex);
        switch(randomIndex)
        {
            case 0:
                return GetStairLeastOpponent();
            case 1:
                return GetStairMostPoint(colorCharacter);
            default:
                return GetStairLeastOpponent();
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
}