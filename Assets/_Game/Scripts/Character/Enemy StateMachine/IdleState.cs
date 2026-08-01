public class IdleState : IEnemyState
{
    public void OnEnter(EnemyAI enemy)
    {
        enemy.ChangeAnim(AnimatorTrigger.IDLE);
    }

    public void OnExecute(EnemyAI enemy)
    {
    }

    public void OnExit(EnemyAI enemy)
    {
    }
}
