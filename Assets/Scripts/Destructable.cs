using UnityEngine;

using System.Collections;
public class Destructable : MonoBehaviour
{
    [SerializeField] private Vector3 particleSize;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject meshIntact;
    [SerializeField] private GameObject meshBroken;
    [SerializeField] private float buildingHeight;
    [SerializeField] private float sinkSpeed;
    [SerializeField] private float shakeFrequency;
    [SerializeField] private float shakeAmount;
    [SerializeField] private Vector3 targetEulerAngles = new Vector3(0f, 30f, 0f);
    [SerializeField] float tiltSpeed;
    private bool sinking;
    private Quaternion targetRotation;
    private void Awake()
    {
        meshBroken.SetActive(false);
        var shape = particleSystem.shape;
        shape.scale = particleSize * 1.3f;
        sinkSpeed += Random.Range(-0.2f, 0.1f);
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
        targetRotation = Quaternion.Euler(targetEulerAngles);
        float distance = Vector3.Distance(position, transform.position);
        //StartCoroutine(Wait(distance / 1000 + Random.Range(0, 0.05f)));
        StartCoroutine(Wait(2f));
    }
    private void Update()
    {
        if(sinking)
        {
            float shakeOffsetX = Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) - 0.5f;
            float shakeOffsetZ = Mathf.PerlinNoise(0f, Time.time * shakeFrequency) - 0.5f;

            Vector3 shakeOffset = new Vector3(shakeOffsetX + Random.Range(-0.1f, 0.1f), 0f, shakeOffsetZ + Random.Range(-0.1f, 0.1f)) * shakeAmount;

            meshIntact.transform.position += (Vector3.down * sinkSpeed * Time.deltaTime) + shakeOffset;

            meshIntact.transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                tiltSpeed * Time.deltaTime
            );

            if (meshIntact.transform.position.y <= -buildingHeight)
            {
                meshIntact.SetActive(false);
                meshBroken.SetActive(true);
            }
        }
    }
    void Break()
    {
        sinking = true;
        particleSystem.Play();
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Break();
    }
}
