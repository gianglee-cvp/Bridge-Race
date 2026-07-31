using UnityEngine;

public class PausedState : IGameState
{
    public void OnChange(GameManager gameManager)
    {
        Time.timeScale = 0;
    }
}
