using UnityEngine;
using System;
using System.Collections;

public class EarthquakeManager : MonoBehaviour
{
    [SerializeField] private GameObject waveSphere;
    [SerializeField] private float waveSpeed;
    [SerializeField] private float waveScale;
    [SerializeField] private MapUI mapUI;
    [SerializeField] private AudioSource audioSource;

    
    private bool isEarthquaking;
    public static EarthquakeManager Instance { get; private set; }
    public event Action<Vector3> OnEarthquake;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartEarthquake()
    {
        transform.position = mapUI.target.position;
        Debug.Log(transform.position);
        StartCoroutine(Wait(5f));
        //waveSphere.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("starting");
            StartEarthquake();
        }
        if(isEarthquaking)
        {
            Vector3 targetScale = new Vector3(waveScale, waveScale, waveScale);
            if (transform.localScale.x < waveScale)
            {
                transform.localScale = Vector3.MoveTowards(
                    transform.localScale,
                    targetScale,
                    waveSpeed * Time.deltaTime
                );
            }
            else
            {
                waveSphere.SetActive(false);
            }
        }
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        
        OnEarthquake?.Invoke(transform.position);
        isEarthquaking = true;
        audioSource.Play();
    }
}
