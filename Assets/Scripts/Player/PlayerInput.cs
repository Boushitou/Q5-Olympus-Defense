using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private bool b_isAzerty = true;

    private void Update()
    {
        MovementInput();
        ConstructionInput();
    }

    private void MovementInput()
    {
        if ((b_isAzerty && Input.GetKey(KeyCode.Z)) || (!b_isAzerty && Input.GetKey(KeyCode.W)))
        {
            _playerMovement.MoveForward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _playerMovement.MoveBackward();
        }

        if ((b_isAzerty && Input.GetKey(KeyCode.Q)) || (!b_isAzerty && Input.GetKey(KeyCode.A)))
        {
            _playerMovement.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _playerMovement.MoveRight();
        }

        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
            _playerMovement.MoveZoom(scroll);
    }

    private void ConstructionInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            
        }
        else if (Input.GetMouseButtonUp(1))
        {
            
        }
    }
}
