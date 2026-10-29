using System;
using UnityEngine;

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
        _inputAction.Game.FirstLane.performed += _ => JudgeInputEvent(0);
        _inputAction.Game.SecondLane.performed += _ => JudgeInputEvent(1);
        _inputAction.Game.ThirdLane.performed += _ => JudgeInputEvent(2);
        _inputAction.Game.FourthLane.performed += _ => JudgeInputEvent(3);
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Game.FirstLane.performed -= _ => JudgeInputEvent(0);
        _inputAction.Game.SecondLane.performed -= _ => JudgeInputEvent(1);
        _inputAction.Game.ThirdLane.performed -= _ => JudgeInputEvent(2);
        _inputAction.Game.FourthLane.performed -= _ => JudgeInputEvent(3);
    }
}
