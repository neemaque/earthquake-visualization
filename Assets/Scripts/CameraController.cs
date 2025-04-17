using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float zoomSpeed = 10f;

    [SerializeField] private float minZoom = 20f;
    [SerializeField] private float maxZoom = 60f;
    [SerializeField] private float minAngle = 30f;
    [SerializeField] private float maxAngle = 80f;

    [SerializeField] private Vector2 mapLimitsMin = new Vector2(-50, -50);
    [SerializeField] private Vector2 mapLimitsMax = new Vector2(50, 50);

    [SerializeField] private float currentZoom = 40f;
    [SerializeField] private float yaw = 0f;

    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 dragOrigin;

    void Start()
    {
        cam = Camera.main;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        UpdateCameraPosition();
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = GetPlanePosition(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = GetPlanePosition(Input.mousePosition);
            Vector3 difference = dragOrigin - currentPos;
            transform.position += difference;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            currentZoom -= scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        float angle = Mathf.Lerp(minAngle, maxAngle, (currentZoom - minZoom) / (maxZoom - minZoom));
        Quaternion rotation = Quaternion.Euler(angle, yaw, 0);

        Vector3 direction = rotation * Vector3.back;
        Vector3 targetPosition = transform.position;

        cam.transform.position = targetPosition + direction * currentZoom;
        cam.transform.rotation = rotation;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, mapLimitsMin.x, mapLimitsMax.x);
        pos.z = Mathf.Clamp(pos.z, mapLimitsMin.y, mapLimitsMax.y);
        transform.position = pos;
    }

    Vector3 GetPlanePosition(Vector3 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        if (ground.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }
}
