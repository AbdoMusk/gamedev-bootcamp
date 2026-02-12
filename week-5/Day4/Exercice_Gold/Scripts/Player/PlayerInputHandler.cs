using UnityEngine;

/// <summary>
/// Handles player input only
/// Delegates to other systems (no logic here)
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerCombat combat;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();

        if (movement == null)
            Debug.LogError("Player missing PlayerMovement component!");
        if (combat == null)
            Debug.LogError("Player missing PlayerCombat component!");
    }

    private void Update()
    {
        if (movement != null) HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.SetMoveDirection(input);
    }

}
