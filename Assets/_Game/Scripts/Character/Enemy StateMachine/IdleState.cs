public class IdleState : IEnemyState
{
    public void OnEnter(EnemyAI enemy)
    {
        enemy.ChangeAnim(AnimatorTrigger.IDLE);
        enemy.DisableAgent();
    }

    public void OnExecute(EnemyAI enemy)
    {
    }

    public void OnExit(EnemyAI enemy)
    {
        enemy.EnableAgent();
    }
}
