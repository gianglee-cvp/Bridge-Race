using UnityEngine;

public class PatrolState : IEnemyState
{
    float timer;
    float randomTime;
    public void OnEnter(EnemyAI enemy)
    {
        timer = 0f;
        randomTime = Random.Range(9f, 18f); 
    }

    public void OnExecute(EnemyAI enemy)
    {
        if (timer >= randomTime || enemy.currentStage.CountActiveBricks() == 0)
        {
            enemy.ChangeState(new BuildState());
            return;
        }

        Transform des = enemy.currentStage.GetActiveBrick();
        enemy.SetAgentDestination(des.position);
        Debug.Log("PatrolState: Moving towards destination: " + des.position);
        
        timer += Time.deltaTime;

    }

    public void OnExit(EnemyAI enemy)
    {
        // Cleanup or transition logic when exiting the patrol state
    }
}