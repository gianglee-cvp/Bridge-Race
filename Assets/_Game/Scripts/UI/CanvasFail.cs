using System;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText; 
    public void SetBestScore(int coin)
    {
        coinText.text = coin.ToString(); 
    }
    public void MainMenuButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>(); 
    }
}
