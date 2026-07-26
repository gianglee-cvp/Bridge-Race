using System;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    public override void Setup()
    {
        base.Setup();
        GameManager.Instance.SetTimeScale(0); 
        int score = GameManager.Instance.player.Point;
        SetBestScore(score); 
    }
    public void SetBestScore(int coin)
    {
        coinText.text = coin.ToString(); 
    }
    public override void PlayButton()
    {
        GameManager.Instance.OnEnd(); 
        base.PlayButton();
    }
}
