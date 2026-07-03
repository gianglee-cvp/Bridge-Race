using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;

    public override void Setup()
    {
        base.Setup();
        UpdateCoin(0); 
    }
    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString(); 
    }
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this); 
    }
}
