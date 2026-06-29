using UnityEngine;

public class BuildState : IEnemyState
{
    private Stair choosenStair;

    public void OnEnter(EnemyAI enemy)
    {
        choosenStair = enemy.ChooseStrategy();
        Vector3 destination = choosenStair.GetLastStepPosition();
        enemy.SetAgentDestination(destination);
    }

    public void OnExecute(EnemyAI enemy)
    {
        // Implement idle behavior here
        if(choosenStair == null)
        {
            Debug.LogError("BuildState: choosenStair is null. Cannot set destination.");
            return; 
        }
        if(enemy.listBricks.Count == 0){
            enemy.ChangeState(new PatrolState());
        }        
        
    }

    public void OnExit(EnemyAI enemy)
    {
        // Cleanup or transition logic when exiting the idle state
    }
}