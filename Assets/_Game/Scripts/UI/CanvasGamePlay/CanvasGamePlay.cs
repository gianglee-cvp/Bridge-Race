using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] RectTransform joystick;
    [SerializeField] CanvasGroup stickCanvasGroup;
    [SerializeField] CountdownUI countdownUI;

    public override void Setup()
    {
        base.Setup();
        UpdateCoin(0);
        countdownUI.gameObject.SetActive(true);

        if (stickCanvasGroup != null)
        {
            stickCanvasGroup.alpha = 0;
            stickCanvasGroup.interactable = false;
            stickCanvasGroup.blocksRaycasts = false;
        }

        joystick.gameObject.SetActive(true);
    }

    public override void Open()
    {
        base.Open();
        countdownUI.PlayCountdown(GameManager.Instance.StartGameplayAfterCountdown);
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
