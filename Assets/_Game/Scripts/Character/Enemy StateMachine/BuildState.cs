using UnityEngine;

public class BuildState : IEnemyState
{
    private Stair choosenStair;

    public void OnEnter(EnemyAI enemy)
    {
        choosenStair = enemy.ChooseStrategy();
        Vector3 destination = choosenStair.GetLastStepPosition();
        enemy.SetAgentDestination(destination);

        enemy.SetAnim(ENUM_ANIMATOR_TRIGGER.RUN);
    }

    public void OnExecute(EnemyAI enemy)
    {
        if(choosenStair == null)
        {
            Debug.LogError("BuildState: choosenStair is null. Cannot set destination.");
            return; 
        }
        
        if(enemy.listBricks.Count == 0 || !enemy.CanMoveUp){
            enemy.ChangeState(new PatrolState());
            if (!enemy.canMoveUp)
            {
                Debug.Log("abc"); 
            }
        }        
        
    }

    public void OnExit(EnemyAI enemy)
    {
        enemy.agent.ResetPath(); 
        enemy.agent.velocity = Vector3.zero;    
    }
}