using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        #if !UNITY_EDITOR
        UpdateBinding(_playerInput.currentActionMap, "PauseAction", "<Keyboard>/escape");
        #endif
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _playerMovement.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        float zoom = -context.ReadValue<float>();
        zoom = zoom > 0 ? 1 : zoom < 0 ? -1 : 0;
        _playerMovement.SetZoom(zoom);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        MenusButtons.Instance.Pause();
    }

    private void UpdateBinding(InputActionMap actionMap, string actionName, string overridePath)
    {
        InputBinding newBinding = new InputBinding();
        int index = 0;
        foreach (InputBinding inputBinding in actionMap.bindings)
        {
            if (inputBinding.action.Equals(actionName))
            {
                newBinding = inputBinding;
                newBinding.overridePath = overridePath;
                break;
            }

            index++;
        }
        actionMap.ApplyBindingOverride(index, newBinding);
    }
}
