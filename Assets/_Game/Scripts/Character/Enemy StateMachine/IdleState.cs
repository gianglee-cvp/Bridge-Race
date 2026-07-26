public class IdleState : IEnemyState
{
    public void OnEnter(EnemyAI enemy)
    {
        enemy.SetAnim(ENUM_ANIMATOR_TRIGGER.IDLE);
    }

    public void OnExecute(EnemyAI enemy)
    {
    }

    public void OnExit(EnemyAI enemy)
    {
    }
}
