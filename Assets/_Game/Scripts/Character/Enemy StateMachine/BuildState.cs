using UnityEngine;

public class BuildState : IEnemyState
{
    private Stair chosenStair;

    public void OnEnter(EnemyAI enemy)
    {
        chosenStair = enemy.ChooseStrategy();
        Vector3 destination = chosenStair.GetLastStepPosition();
        enemy.SetAgentDestination(destination);

        enemy.SetAnim(AnimatorTrigger.RUN);
    }

    public void OnExecute(EnemyAI enemy)
    {
        if(enemy.listBricks.Count == 0 && !enemy.CanMoveUp){
            enemy.ChangeState(new PatrolState());
        }        
        
    }

    public void OnExit(EnemyAI enemy)
    {
        enemy.agent.velocity = Vector3.zero;    
    }
}