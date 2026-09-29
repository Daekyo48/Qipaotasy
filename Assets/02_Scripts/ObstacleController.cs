using UnityEngine;
using UnityEngine.InputSystem;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private InputActions _inputAction;

    private Vector3 _moveInput;

    private void Awake()
    {
        _inputAction = new InputActions();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Obstacle.Move.performed += OnMove;
        _inputAction.Obstacle.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Obstacle.Move.performed -= OnMove;
        _inputAction.Obstacle.Move.canceled -= OnMove;
    }

    private void FixedUpdate()
    {
        transform.position += _moveInput * _speed;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }
}
