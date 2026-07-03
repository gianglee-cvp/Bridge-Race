using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasVictory : UICanvas
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
