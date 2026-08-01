using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText; 
    [SerializeField] private Star star;
    protected int seed;
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
        star.PlayAnim(seed);
        // Debug.Log(seed);
    }
    public override void Close(float time)
    {
        base.Close(time);
        star.OnClose(); 
    }
    public void InitSeed(int m_seed)
    {
        seed = m_seed;
    }
}
