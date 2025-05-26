using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject openMapButton;
    [SerializeField] private RectTransform circle;
    [SerializeField] private RectTransform pulsator;
    [SerializeField] private float growRate = 1f;
    [SerializeField] private Vector3 targetScale = new Vector3(500f, 500f, 500f);
    private bool quaking = false;
    private bool a;

    public void openMap()
    {
        mapPanel.SetActive(true);
        openMapButton.SetActive(false);
    }
    public void closeMap()
    {
        mapPanel.SetActive(false);
    }
    public void Earthquake()
    {
        quaking = true;
    }
    private void Update()
    {
        if(quaking)
        {
            pulsator.localScale = new Vector3(0,0,0);
            if (circle.localScale.x < targetScale.x)
            {
                circle.localScale = Vector3.MoveTowards(
                    circle.localScale,
                    targetScale,
                    growRate * Time.deltaTime
                );
            }
        }
        else
        {
            if(pulsator.localScale.x >= 0.5f && !a) a = true;
            else if(pulsator.localScale.x <= 0.1f && a) a = false;
            if(pulsator.localScale.x < 0.5f && !a)
            {
                pulsator.localScale = Vector3.MoveTowards(
                    pulsator.localScale,
                    new Vector3(0.5f,0.5f,0.5f),
                    0.2f * Time.deltaTime
                );
            }
            else if(pulsator.localScale.x > 0.1f && a)
            {
                pulsator.localScale = Vector3.MoveTowards(
                    pulsator.localScale,
                    new Vector3(0.1f,0.1f, 0.1f),
                    0.2f * Time.deltaTime
                );
            }
        }
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        
        a =false;
    }
}
