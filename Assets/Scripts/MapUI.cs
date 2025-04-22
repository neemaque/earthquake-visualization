using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapUI : MonoBehaviour, IPointerClickHandler
{
    private Vector2 worldSize = new Vector2(100f, 100f);
    public Transform target;

    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<RectTransform>();

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            Vector2 normalized = new Vector2(
                (localPoint.x + rt.rect.width / 2f) / rt.rect.width,
                (localPoint.y + rt.rect.height / 2f) / rt.rect.height
            );

            Vector3 worldPos = new Vector3(
                normalized.x * worldSize.x,
                0f,
                normalized.y * worldSize.y
            );

            if (target != null)
            {
                target.position = worldPos;
                Debug.Log(target.position);
            }
        }
    }
}
