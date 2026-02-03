using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 6f;

    [Header("Perspective Mode")]
    [SerializeField] private float perspectiveOffsetX = 2f;
    [SerializeField] private float perspectiveOffsetY = 3f;
    [SerializeField] private float perspectiveOffsetZ = -20f;
    [SerializeField] private float perspectiveRotationY = 0f;

    [Header("Orthographic Mode")]
    [SerializeField] private float orthographicOffsetX = 0f;
    [SerializeField] private float orthographicOffsetY = 5f;
    [SerializeField] private float orthographicOffsetZ = -10f;
    [SerializeField] private float orthographicRotationX = 10f;
    [SerializeField] private float orthographicRotationY = 25f;
    [SerializeField] private float orthographicSize = 5f;
    [SerializeField] private float nearClipPlane = -10f;

    [Header("Shake")]
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeAmplitude = 0.25f;
    [SerializeField] private float shakeFrequency = 25f;

    private bool isPerspective = true;
    private Camera cam;
    private Vector3 shakeOffset = Vector3.zero;
    private Coroutine shakeRoutine;

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                target = player.transform;
        }
        SetPerspectiveMode();
    }

    private void Update()
    {
        if (Keyboard.current?.cKey.wasPressedThisFrame == true)
        {
            ToggleCameraMode();
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 offsetPos = isPerspective 
            ? new Vector3(perspectiveOffsetX, perspectiveOffsetY, perspectiveOffsetZ)
            : new Vector3(orthographicOffsetX, orthographicOffsetY, orthographicOffsetZ);

        Vector3 desiredPos = new Vector3(
            target.position.x + offsetPos.x,
            Mathf.Clamp(target.position.y + offsetPos.y, minY, maxY),
            target.position.z + offsetPos.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime) + shakeOffset;
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        float targetRotX = isPerspective ? 0f : orthographicRotationX;
        float targetRotY = isPerspective ? perspectiveRotationY : orthographicRotationY;
        
        Vector3 currentEuler = transform.eulerAngles;
        currentEuler.x = Mathf.Lerp(currentEuler.x, targetRotX, smoothSpeed * Time.deltaTime);
        currentEuler.y = Mathf.Lerp(currentEuler.y, targetRotY, smoothSpeed * Time.deltaTime);
        transform.eulerAngles = currentEuler;
    }

    private void ToggleCameraMode()
    {
        if (isPerspective)
            SetOrthographicMode();
        else
            SetPerspectiveMode();
    }

    public void ToggleCamera()
    {
        ToggleCameraMode();
    }

    private void SetPerspectiveMode()
    {
        isPerspective = true;
        cam.orthographic = false;
        cam.nearClipPlane = 0.01f;
        Debug.Log("Camera: Perspective Mode");
    }

    private void SetOrthographicMode()
    {
        isPerspective = false;
        cam.orthographic = true;
        cam.orthographicSize = orthographicSize;
        cam.nearClipPlane = nearClipPlane;
        Debug.Log("Camera: Orthographic Mode");
    }

    public void TriggerShake()
    {
        if (shakeRoutine != null) StopCoroutine(shakeRoutine);
        shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float strength = shakeAmplitude * (1f - (elapsed / shakeDuration));
            float angle = Time.time * shakeFrequency;
            float x = Mathf.Sin(angle) * strength;
            float y = Mathf.Cos(angle * 1.3f) * strength;
            shakeOffset = new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
        shakeRoutine = null;
    }
}
