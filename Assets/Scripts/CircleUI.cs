using UnityEngine;

public class CircleUI : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private float growRate = 1f;
    [SerializeField] private Vector3 targetScale = new Vector3(500f, 500f, 500f);
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Activate()
    {
        active = true;
    }
    void Update()
    {
        if (rect.localScale.x < targetScale.x && active)
        {
            rect.localScale = Vector3.MoveTowards(
                rect.localScale,
                targetScale,
                growRate * Time.deltaTime
            );
        }
    }
}
