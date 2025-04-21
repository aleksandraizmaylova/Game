using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        Instance = this;
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    
    public Vector2 GetMovementVector() => playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    public bool IsSprinting() => playerInputActions.Player.Sprint.ReadValue<float>() > Constants.Inaccuracy;
    public bool IsSneaking() => playerInputActions.Player.Sneak.ReadValue<float>() > Constants.Inaccuracy;
}
