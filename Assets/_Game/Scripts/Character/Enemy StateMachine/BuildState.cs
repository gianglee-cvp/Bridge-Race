using UnityEngine;

public class BuildState : IEnemyState
{
    public void OnEnter(EnemyAI enemy)
    {
        enemy.StopAgent();
        Bridge chosenStair;
        chosenStair = enemy.ChooseStrategy();
        Vector3 destination = chosenStair.GetLastStepPosition();
        enemy.SetAgentDestination(destination);

        enemy.ChangeAnim(AnimatorTrigger.RUN);
    }

    public void OnExecute(EnemyAI enemy)
    {
        if(enemy.CurrentBrickCount == 0 && !enemy.CanMoveUp){
            enemy.SetPatrolState();
        }        
        
    }

    public void OnExit(EnemyAI enemy)
    {
        // enemy.agent.velocity = Vector3.zero;    
        enemy.StopAgent();
    }
}