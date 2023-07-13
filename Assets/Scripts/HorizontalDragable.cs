using UnityEngine;
using UnityEngine.EventSystems;

public class HorizontalDragable : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;
    private float startPositionX;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        delta.y = 0f; // Ignore vertical movement

        rectTransform.anchoredPosition += delta;
    }
}