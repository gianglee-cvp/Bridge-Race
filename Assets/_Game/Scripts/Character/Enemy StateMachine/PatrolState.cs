using UnityEngine;

public class PatrolState : IEnemyState
{
    public void OnEnter(EnemyAI enemy)
    {
        enemy.InitPatrolState();
    }

    public void OnExecute(EnemyAI enemy)
    {
        enemy.ExecutePatrol();
    }

    public void OnExit(EnemyAI enemy)
    {
    }
}