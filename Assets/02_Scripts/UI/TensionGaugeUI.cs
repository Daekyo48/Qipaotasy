using UnityEngine;
using UnityEngine.UI;

public class TensionGaugeUI : MonoBehaviour
{
    [Header("# Event Channel")]
    [SerializeField] private JudgmentTypeEventChannel _judgeEvent;

    [Header("Components")]
    [SerializeField] private Slider _tensionGauge;

    [Header("Settings")]
    [SerializeField] private float _maxValue = 100f;

    private float _gauge;

    private void OnEnable()
    {
        _judgeEvent.OnEventRaised += RefreshGauge;
    }

    private void OnDisable()
    {
        _judgeEvent.OnEventRaised -= RefreshGauge;
    }

    private void RefreshGauge(JudgmentType judgment)
    {
        if (judgment == JudgmentType.Perfect)
        {
            _gauge += 10;
        }

        if (judgment == JudgmentType.Great)
        {
            _gauge += 5;
        }

        _tensionGauge.value = _gauge / _maxValue;

        if (_gauge / _maxValue >= 1f)
        {
            print("[ 스킬 발동 가능 ]");
            _tensionGauge.value = 0f;
        }
    }
}
