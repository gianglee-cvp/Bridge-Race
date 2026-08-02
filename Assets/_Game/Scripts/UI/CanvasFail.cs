using System;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText; 
    public override void Setup()
    {
        base.Setup();
        SoundManager.Instance.PlaySfx(ENUM_SOUND.Fail);
        int score = LevelManager.Instance.Player.Point;
        SetBestScore(score, coinText); 
    }
}
