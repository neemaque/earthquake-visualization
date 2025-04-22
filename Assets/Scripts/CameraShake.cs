using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    private float shakeTimer = 0f;
    private Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
    }
    IEnumerator Start()
    {
        yield return null;

        if (EarthquakeManager.Instance != null)
            EarthquakeManager.Instance.OnEarthquake += HandleEvent;
    }

    void OnDisable()
    {
        if (EarthquakeManager.Instance != null)
            EarthquakeManager.Instance.OnEarthquake -= HandleEvent;
    }

    void HandleEvent(Vector3 position)
    {
        originalPos = transform.localPosition;
        TriggerShake();
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * magnitude;
            transform.localPosition = originalPos + randomOffset;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            //transform.localPosition = originalPos;
        }
    }

    public void TriggerShake()
    {
        shakeTimer = duration;
    }
}
