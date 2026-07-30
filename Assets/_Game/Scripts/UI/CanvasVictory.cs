using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText; 
    public override void Setup()
    {
        base.Setup();
        SoundManager.Instance.PlaySfx(ENUM_SOUND.Win);
        int score = LevelManager.Instance.Player.Point;
        SetBestScore( score, coinText); 
    }
}
