using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText; 
    [SerializeField] private Star star;
    public override void Setup()
    {
        base.Setup();
        SoundManager.Instance.PlaySfx(ENUM_SOUND.Win);
        int score = LevelManager.Instance.Player.Point;
        SetBestScore( score, coinText); 
    }
    public override void Open()
    {
        base.Open();
        star.PlayAnim(1); 
    }
    public override void Close(float time)
    {
        base.Close(time);
        star.OnClose(); 
    }
}
