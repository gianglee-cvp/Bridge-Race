using UnityEngine;

public class IdleState : IEnemyState
{
    public void OnEnter(EnemyAI enemy)
    {
        enemy.characterCollider.enabled = false;
    }

    public void OnExecute(EnemyAI enemy)
    {
        // Implement idle behavior here
    }

    public void OnExit(EnemyAI enemy)
    {
        // Cleanup or transition logic when exiting the idle state
        enemy.characterCollider.enabled = true;
    }
}