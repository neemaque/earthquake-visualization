using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject openMapButton;
    [SerializeField] private RectTransform circle;
    [SerializeField] private float growRate = 1f;
    [SerializeField] private Vector3 targetScale = new Vector3(500f, 500f, 500f);
    private bool quaking = false;

    public void openMap()
    {
        mapPanel.SetActive(true);
        openMapButton.SetActive(false);
    }
    public void closeMap()
    {
        mapPanel.SetActive(false);
        openMapButton.SetActive(true);
    }
    public void Earthquake()
    {
        quaking = true;
    }
    private void Update()
    {
        if(quaking)
        {
            if (circle.localScale.x < targetScale.x)
            {
                circle.localScale = Vector3.MoveTowards(
                    circle.localScale,
                    targetScale,
                    growRate * Time.deltaTime
                );
            }
        }
    }
}
