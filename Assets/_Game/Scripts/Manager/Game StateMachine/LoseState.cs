using UnityEngine;

public class LoseState : IGameState
{
    public void OnChange(GameManager gameManager)
    {
        Time.timeScale = 0;
        UIManager.Instance.OpenUI<CanvasFail>();
    }
}
