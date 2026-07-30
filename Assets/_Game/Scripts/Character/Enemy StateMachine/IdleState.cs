public class IdleState : IEnemyState
{
    public void OnEnter(EnemyAI enemy)
    {
        enemy.SetAnim(AnimatorTrigger.IDLE);
    }

    public void OnExecute(EnemyAI enemy)
    {
    }

    public void OnExit(EnemyAI enemy)
    {
    }
}
