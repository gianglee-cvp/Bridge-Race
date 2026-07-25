using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText; 

    public void SetBestScore(int coin)
    {
        coinText.text = coin.ToString(); 
    }
}
