using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] RectTransform joinStick;
    [SerializeField] CanvasGroup stickCanvasGroup;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] GameObject stick;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
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