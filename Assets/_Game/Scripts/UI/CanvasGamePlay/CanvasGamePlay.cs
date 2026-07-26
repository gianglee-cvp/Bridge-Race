using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] RectTransform joinStick;
    [SerializeField] CanvasGroup stickCanvasGroup;

    public override void Setup()
    {
        base.Setup();
        UpdateCoin(0); 
        
  
        stickCanvasGroup.alpha = 0;
        joinStick.gameObject.SetActive(true); 
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
