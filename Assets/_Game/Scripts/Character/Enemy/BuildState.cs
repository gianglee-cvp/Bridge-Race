using UnityEngine;

public class BuildState : IEnemyState
{
    private Stair choosenStair;
    private int currentStepIndex;
    
    // Cờ để biết đã set destination cho step hiện tại chưa
    // → tránh gọi SetDestination mỗi frame
    private bool hasSetDestination;

    public void OnEnter(EnemyAI enemy)
    {
        choosenStair = enemy.ChooseStrategy();
        currentStepIndex = 0;
        hasSetDestination = false;
    }

    public void OnExecute(EnemyAI enemy)
    {
        if (choosenStair == null)
        {
            Debug.LogError("BuildState: choosenStair is null. Cannot set destination.");
            enemy.ChangeState(new PatrolState());
            return;
        }

        // Hết step → quay lại patrol
        if (currentStepIndex >= choosenStair.steps.Count)
        {
            Debug.Log("BuildState: Đã đi hết tất cả step. Chuyển sang PatrolState.");
            enemy.ChangeState(new PatrolState());
            return;
        }

        // BƯỚC 1: Set destination CHỈ 1 LẦN cho step hiện tại
        if (!hasSetDestination)
        {
            Step targetStep = choosenStair.steps[currentStepIndex];
            enemy.SetAgentDestination(targetStep.transform.position);
            hasSetDestination = true;
            
            Debug.Log($"BuildState: Đi đến step [{currentStepIndex}] - {targetStep.name}");
            return; // Đợi frame sau để check
        }

        // BƯỚC 2: Mỗi frame chỉ CHECK đã đến step hiện tại chưa
        if (enemy.HasReachedDestination())
        {
            Debug.Log($"BuildState: Đã đến step [{currentStepIndex}]!");
            
            // BƯỚC 3: Chuyển sang step tiếp theo
            currentStepIndex++;
            hasSetDestination = false; // Reset cờ → frame sau sẽ set destination mới
        }
    }

    public void OnExit(EnemyAI enemy)
    {
        // Cleanup or transition logic when exiting the build state
    }
}