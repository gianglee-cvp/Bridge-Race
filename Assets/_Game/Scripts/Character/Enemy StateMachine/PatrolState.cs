using UnityEngine;

public class PatrolState : IEnemyState
{
    float timer;
    float randomTime;
    bool isGoToDestination;
    Transform des ;
    Vector2 desXZ ;
    public Vector2 GetVector2XZ(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
    public void OnEnter(EnemyAI enemy)
    {
        timer = 0f;
        randomTime = Random.Range(5f, 18f); 
        enemy.SetAnim(AnimatorTrigger.RUN);
        enemy.CanMoveUp = true; 
    }

    public void OnExecute(EnemyAI enemy)
    {

        if (timer >= randomTime || enemy.currentStage.CountActiveBricks(enemy.colorCharacter) == 0)
        {
            enemy.ChangeState(new BuildState());
            timer = 0f;
            return;
        }

        if(!isGoToDestination)
        {
            isGoToDestination = true;

            des = enemy.currentStage.GetActiveBrick(enemy.colorCharacter);
            desXZ = GetVector2XZ(des.position);

            enemy.SetAgentDestination(des.position);
        }

        Vector2 enemyXZ = GetVector2XZ(enemy.transform.position);
        if(Vector2.Distance(enemyXZ, desXZ) < 0.1f)
        {
            isGoToDestination = false;  
        }

        timer += Time.deltaTime;

    }

    public void OnExit(EnemyAI enemy)
    {
    }
}