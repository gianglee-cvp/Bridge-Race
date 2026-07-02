using UnityEngine;

public class PatrolState : IEnemyState
{
    float timer;
    float randomTime;
    bool isGoToDestination;
    Transform des ;
    Vector2 desXZ ;
    public void OnEnter(EnemyAI enemy)
    {
        timer = 0f;
        randomTime = Random.Range(5f, 18f); 
        enemy.SetAnim(ENUM_ANIMATOR_TRIGGER.RUN);
    }

    public void OnExecute(EnemyAI enemy)
    {

        if (timer >= randomTime || enemy.currentStage.CountActiveBricks() == 0)
        {
            enemy.ChangeState(new BuildState());
            timer = 0f;
            return;
        }

        if(!isGoToDestination)
        {
            isGoToDestination = true;

            des = enemy.currentStage.GetActiveBrick(enemy.colorCharacter);
            desXZ = GameManager.Instance.GetVector2XZ(des.position);

            enemy.SetAgentDestination(des.position);
            // /Debug.Log("PatrolState: Moving towards destination: " + des.position);
        }

        Vector2 enemyXZ = GameManager.Instance.GetVector2XZ(enemy.transform.position);
        if(Vector2.Distance(enemyXZ, desXZ) < 0.1f)
        {
            isGoToDestination = false;  
        }

        timer += Time.deltaTime;

    }

    public void OnExit(EnemyAI enemy)
    {
        // Cleanup or transition logic when exiting the patrol state
    }
}