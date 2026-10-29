using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-10)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event Action SkillInput;
    public event Action<int> LaneJudgmentInput;
    
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
        _inputAction.Game.Skill.performed       += OnSkill;
        _inputAction.Game.FirstLane.performed   += OnFirstLane;
        _inputAction.Game.SecondLane.performed  += OnSecondLane;
        _inputAction.Game.ThirdLane.performed   += OnThirdLane;
        _inputAction.Game.FourthLane.performed  += OnFourthLane;
        _inputAction.Game.SpeedDown.performed   += OnSpeedDown;
        _inputAction.Game.SpeedUp.performed     += OnSpeedUp;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Game.Skill.performed       -= OnSkill;
        _inputAction.Game.FirstLane.performed   -= OnFirstLane;
        _inputAction.Game.SecondLane.performed  -= OnSecondLane;
        _inputAction.Game.ThirdLane.performed   -= OnThirdLane;
        _inputAction.Game.FourthLane.performed  -= OnFourthLane;
        _inputAction.Game.SpeedDown.performed   -= OnSpeedDown;
        _inputAction.Game.SpeedUp.performed     -= OnSpeedUp;
    }

    private void OnSkill(InputAction.CallbackContext _)
    {
        SkillInput?.Invoke();
    }

    private void OnFirstLane(InputAction.CallbackContext _)
    {
        LaneJudgmentInput?.Invoke(0);
    }

    private void OnSecondLane(InputAction.CallbackContext _)
    {
        LaneJudgmentInput?.Invoke(1);
    }

    private void OnThirdLane(InputAction.CallbackContext _)
    {
        LaneJudgmentInput?.Invoke(2);
    }

    private void OnFourthLane(InputAction.CallbackContext _)
    {
        LaneJudgmentInput?.Invoke(3);
    }

    private void OnSpeedDown(InputAction.CallbackContext _)
    {
        GameManager.Instance.SettingData.UserSpeed -= 2f;
    }

    private void OnSpeedUp(InputAction.CallbackContext _)
    {
        GameManager.Instance.SettingData.UserSpeed += 2f;
    }
}
