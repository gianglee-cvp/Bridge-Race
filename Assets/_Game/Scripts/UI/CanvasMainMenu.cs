using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    public override void Setup()
    {
        base.Setup();
    }
    public void PlayButton()
    {
        Close(0); 

        UIManager.Instance.OpenUI<CanvasGamePlay>();

        int index = GameManager.Instance.currentLevelIndex ; 
        GameManager.Instance.OnChangeLevel(index);
        GameManager.Instance.OnPlayGame();
    }
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this); 
    }

    public void NextLevelButton()
    {
        GameManager.Instance.NextLevel();
    }

    public void PrevLevelButton()
    {
        GameManager.Instance.PrevLevel();
    }
    public void UpdateLevelText(int index)
    {
        levelText.text = "Level " + (index + 1).ToString();
    }
}
