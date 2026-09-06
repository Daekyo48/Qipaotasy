using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("# References")]
    [SerializeField] private NoteJudge _judge;

    private InputActions _inputAction;

    private void Awake()
    {
        _inputAction = new InputActions();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Game.Judge.performed += OnJudge;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Game.Judge.performed -= OnJudge;
    }

    private void OnJudge(InputAction.CallbackContext context)
    {
        int lane = context.action.GetBindingIndexForControl(context.control);

        _judge.Judge(lane);
    }
}
