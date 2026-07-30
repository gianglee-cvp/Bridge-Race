using System;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText; 
    public override void Setup()
    {
        base.Setup();
        GameManager.Instance.SetTimeScale(0); 
        SoundManager.Instance.PlaySfx(ENUM_SOUND.Fail);
        int score = LevelManager.Instance.Player.Point;
        SetBestScore(score, coinText); 
    }
}
