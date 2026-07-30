using UnityEngine;
using UnityEngine.EventSystems;

public class TouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] RectTransform joinStick;
    [SerializeField] CanvasGroup stickCanvasGroup;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] GameObject stick;

    private void Awake()
    {
        SetStickVisible(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;
        SetStickVisible(true);

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

        SetStickVisible(false);
    }

    public void SetStickVisible(bool isVisible)
    {
        if (stickCanvasGroup == null)
        {
            return;
        }

        stickCanvasGroup.alpha = isVisible ? 1f : 0f;
        stickCanvasGroup.interactable = isVisible;
        stickCanvasGroup.blocksRaycasts = isVisible;
    }
}
