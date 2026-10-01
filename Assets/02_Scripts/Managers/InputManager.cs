using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event Action<int> JudgeInputEvent;
    
    private InputActions _inputAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _inputAction = new InputActions();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Game.FirstLane.performed += OnFirstLane;
        _inputAction.Game.SecondLane.performed += OnSecondLane;
        _inputAction.Game.ThirdLane.performed += OnThirdLane;
        _inputAction.Game.FourthLane.performed += OnFourthLane;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Game.FirstLane.performed -= OnFirstLane;
        _inputAction.Game.SecondLane.performed -= OnSecondLane;
        _inputAction.Game.ThirdLane.performed -= OnThirdLane;
        _inputAction.Game.FourthLane.performed -= OnFourthLane;
    }

    private void OnFirstLane(InputAction.CallbackContext context)
    {
        JudgeInputEvent?.Invoke(0);
    }

    private void OnSecondLane(InputAction.CallbackContext context)
    {
        JudgeInputEvent?.Invoke(1);
    }

    private void OnThirdLane(InputAction.CallbackContext context)
    {
        JudgeInputEvent?.Invoke(2);
    }

    private void OnFourthLane(InputAction.CallbackContext context)
    {
        JudgeInputEvent?.Invoke(3);
    }
}
