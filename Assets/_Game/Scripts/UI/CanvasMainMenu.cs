using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    public override void Setup()
    {
        base.Setup();
        SoundManager.Instance.PlayBgm(ENUM_SOUND.MenuBgm);
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this); 
    }

    public void NextLevelButton()
    {
        LevelManager.Instance.NextLevel();
    }

    public void PrevLevelButton()
    {
        LevelManager.Instance.PrevLevel();
    }
    public void UpdateLevelText(int index)
    {
        levelText.text = "Level " + (index + 1).ToString();
    }
}
