using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasGamePlay : UICanvas, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] RectTransform joinStick;
    [SerializeField] CanvasGroup stickCanvasGroup;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] GameObject stick;

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

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;
        stickCanvasGroup.alpha = 1; 

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint);

        joinStick.anchoredPosition = localPoint;

        // Truyền event xuống cho OnScreenStick
        ExecuteEvents.Execute<IPointerDownHandler>(
            stick,
            eventData,
            ExecuteEvents.pointerDownHandler
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        ExecuteEvents.Execute<IDragHandler>(
            stick,
            eventData,
            ExecuteEvents.dragHandler
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ExecuteEvents.Execute<IPointerUpHandler>(
            stick,
            eventData,
            ExecuteEvents.pointerUpHandler
        );
        
        // Ẩn joystick bằng alpha thay vì SetActive
        stickCanvasGroup.alpha = 0; 
    }
}
