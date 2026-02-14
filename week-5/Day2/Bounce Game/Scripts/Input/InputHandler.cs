using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController playerController;
    
    void Update()
    {
        if (playerController == null) return;
        
        // Handle horizontal movement (Keyboard: A/D or Left/Right arrows, Controller: Left Stick)
        float horizontal = 0f;
        
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                horizontal = -1f;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                horizontal = 1f;
        }
        
        if (Gamepad.current != null)
        {
            float stickX = Gamepad.current.leftStick.x.ReadValue();
            if (Mathf.Abs(stickX) > 0.1f)
                horizontal = stickX;
        }
        
        playerController.SetHorizontalInput(horizontal);
        
        // Handle boost/dash (Keyboard: Space, Controller: South button / A on Xbox)
        bool boostPressed = false;
        
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            boostPressed = true;
        
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            boostPressed = true;
        
        if (boostPressed)
            playerController.Boost();
    }
}
