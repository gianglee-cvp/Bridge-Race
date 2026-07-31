using UnityEngine;

public class PlayingState : IGameState
{
    public void OnChange(GameManager gameManager)
    {
        Time.timeScale = 1;
    }
}
