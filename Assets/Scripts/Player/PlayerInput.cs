using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public void OnMove(InputAction.CallbackContext context)
    {
        _playerMovement.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        _playerMovement.SetZoom(context.ReadValue<float>());
    }
}
