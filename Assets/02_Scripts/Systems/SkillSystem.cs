using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [Header("# Event Channel")]
    [SerializeField] private JudgmentTypeEventChannel _judgeEvent;

    [Header("# Settings")]
    [SerializeField] private float _maxValue = 100f;

    private float _tension;

    private void OnEnable()
    {
        _judgeEvent.OnEventRaised += UpdateTension;
    }

    private void OnDisable()
    {
        _judgeEvent.OnEventRaised -= UpdateTension;
    }

    private void UpdateTension(JudgmentType judgment)
    {
        switch (judgment)
        {
            case JudgmentType.Perfect:
                _tension += 10;
                break;

            case JudgmentType.Great:
                _tension += 5;
                break;
        }

        if (_tension / _maxValue >= 1f)
        {
            print("[ 스킬 발동 가능 ]");
        }
    }
}
