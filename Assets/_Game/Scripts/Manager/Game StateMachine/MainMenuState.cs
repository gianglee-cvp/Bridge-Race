using UnityEngine;

public class MainMenuState : IGameState
{
    public void OnChange(GameManager gameManager)
    {
        Time.timeScale = 1;
        LevelManager.Instance.OnEnd();
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
