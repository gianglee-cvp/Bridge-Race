using UnityEngine;

public class WinState : IGameState
{
    public void OnChange(GameManager gameManager)
    {
        Time.timeScale = 1;
        GameManager.Instance.SetCameraWin(); 
        UIManager.Instance.OpenUI<CanvasVictory>();
    }
    
}
