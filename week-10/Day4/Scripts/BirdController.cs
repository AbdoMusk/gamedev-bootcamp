using UnityEngine;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float flapForce = 5f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float maxFallSpeed = 10f;
    [SerializeField] private float laneSwitchSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxRotationX = 45f;
    [SerializeField] private float maxRotationZ = 30f;
    [SerializeField] private int laneCount = 2;
    [SerializeField] private float laneSpacing = 2f;
    [SerializeField] private int startLane = 0;
    [SerializeField] private float startPositionX = 0f;
    [SerializeField] private float startPositionY = 5f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 9f;
    [SerializeField] private float diveVelocityThreshold = -5f;

    private const float HalfTurnDegrees = 180f;
    private const float FullTurnDegrees = 360f;
    private const float LaneCenterFactor = 0.5f;

    private float verticalVelocity;
    private int currentLane = 0;
    private int lastLane = 0;
    private float targetXRotation = 0f;
    private bool isAlive = true;
    private bool gameStarted = false;
    private GameManager gameManager;
    private CameraController cameraController;
    [SerializeField] private Animator animator;

    public bool IsAlive => isAlive;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        cameraController = FindFirstObjectByType<CameraController>();
        // animator = GetComponent<Animator>();
        currentLane = Mathf.Clamp(startLane, 0, Mathf.Max(0, laneCount - 1));
        lastLane = currentLane;
        transform.position = new Vector3(startPositionX, startPositionY, GetLaneZ(currentLane));
        SetAnimatorState("isIdle", true);
    }

    private void Update()
    {
        if (!isAlive) return;

        if (!gameStarted)
        {
            if (IsTapPressed())
            {
                StartFromInput();
            }
            return;
        }

        lastLane = currentLane;
        HandleInput();
        verticalVelocity = Mathf.Max(verticalVelocity - gravity * Time.deltaTime, -maxFallSpeed);
        Vector3 pos = transform.position;
        pos.y += verticalVelocity * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        UpdateAnimatorState();
        UpdateRotation();

        if (transform.position.y <= minY)
            Die();
    }

    private void StartFromInput()
    {
        gameStarted = true;
        verticalVelocity = flapForce;
        gameManager?.StartGame();
        SetAnimatorState("isIdle", false);
        SetAnimatorState("isFlying", true);
        AudioEvents.RaiseBirdFlyStart();
    }

    private void HandleInput()
    {
        if (IsTapPressed())
        {
            verticalVelocity = flapForce;
            AudioEvents.RaiseBirdFlyStart();
        }

        if (Keyboard.current?.sKey.wasPressedThisFrame == true)
            currentLane = Mathf.Max(0, currentLane - 1);
        else if (Keyboard.current?.wKey.wasPressedThisFrame == true)
            currentLane = Mathf.Min(Mathf.Max(0, laneCount - 1), currentLane + 1);

        Vector3 pos = transform.position;
        pos.z = Mathf.Lerp(pos.z, GetLaneZ(currentLane), laneSwitchSpeed * Time.deltaTime);
        transform.position = pos;
    }

    private bool IsTapPressed()
    {
        return Keyboard.current?.spaceKey.wasPressedThisFrame == true
            || Mouse.current?.leftButton.wasPressedThisFrame == true
            || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true;
    }

    public void Flap()
    {
        if (!isAlive) return;
        if (!gameStarted)
        {
            StartFromInput();
            return;
        }

        verticalVelocity = flapForce;
        AudioEvents.RaiseBirdFlyStart();
    }

    public void MoveLeft()
    {
        if (!isAlive || !gameStarted) return;
        currentLane = Mathf.Max(0, currentLane - 1);
    }

    public void MoveRight()
    {
        if (!isAlive || !gameStarted) return;
        currentLane = Mathf.Min(Mathf.Max(0, laneCount - 1), currentLane + 1);
    }

    private void UpdateRotation()
    {
        // Rotation on Z axis based on vertical velocity (up/down)
        float zRotation = Mathf.Clamp(verticalVelocity / maxFallSpeed * maxRotationZ, -maxRotationZ, maxRotationZ);

        // Rotation on X axis based on lane change
        float desiredXRotation = 0f;
        if (currentLane > lastLane)
            desiredXRotation = maxRotationX; // Moving right
        else if (currentLane < lastLane)
            desiredXRotation = -maxRotationX; // Moving left

        // Lerp the target rotation (creates smooth decay)
        targetXRotation = Mathf.Lerp(targetXRotation, desiredXRotation, rotationSpeed * Time.deltaTime);

        // Get current rotation and normalize angles
        Vector3 currentEuler = transform.eulerAngles;
        if (currentEuler.x > HalfTurnDegrees) currentEuler.x -= FullTurnDegrees;
        if (currentEuler.z > HalfTurnDegrees) currentEuler.z -= FullTurnDegrees;

        // Create target rotation
        Vector3 targetRotation = new Vector3(targetXRotation, 0f, zRotation);

        // Smooth lerp to target
        currentEuler.x = Mathf.Lerp(currentEuler.x, targetRotation.x, rotationSpeed * Time.deltaTime);
        currentEuler.z = Mathf.Lerp(currentEuler.z, targetRotation.z, rotationSpeed * Time.deltaTime);

        transform.eulerAngles = currentEuler;
    }

    private void UpdateAnimatorState()
    {
        if (verticalVelocity < diveVelocityThreshold)
            SetAnimatorState("isDiving", true);
        else
            SetAnimatorState("isDiving", false);
    }

    private void SetAnimatorState(string triggerName, bool value)
    {
        if (animator == null) return;

        if (value)
            animator.SetTrigger(triggerName);
        else
            animator.ResetTrigger(triggerName);
    }

    public void Die()
    {
        isAlive = false;
        gameStarted = false;
        SetAnimatorState("isFlying", false);
        SetAnimatorState("isDead", true);
        cameraController?.TriggerShake();
        AudioEvents.RaiseBirdHit();
        AudioEvents.RaiseBirdFlyStop();
        gameManager?.GameOver();
    }

    public void ResetBird()
    {
        isAlive = true;
        gameStarted = false;
        currentLane = Mathf.Clamp(startLane, 0, Mathf.Max(0, laneCount - 1));
        lastLane = currentLane;
        verticalVelocity = 0f;
        transform.position = new Vector3(startPositionX, startPositionY, GetLaneZ(currentLane));
        transform.eulerAngles = Vector3.zero;
        SetAnimatorState("isDead", false);
        SetAnimatorState("isIdle", true);
        AudioEvents.RaiseBirdFlyStop();
    }

    private float GetLaneZ(int lane)
    {
        if (laneCount <= 1) return 0f;
        float centerIndex = (laneCount - 1) * LaneCenterFactor;
        return (lane - centerIndex) * laneSpacing;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
            Die();
        else if (other.CompareTag("ScoreZone"))
        {
            gameManager?.AddScore();
            AudioEvents.RaiseScore();
        }
    }
}
