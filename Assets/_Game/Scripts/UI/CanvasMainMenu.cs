using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public override void Setup()
    {
        base.Setup();
    }
    public void PlayButton()
    {
        Close(0); 
        UIManager.Instance.OpenUI<CanvasGamePlay>(); 
        GameManager.Instance.OnPlayGame();
    }
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this); 
    }
}
