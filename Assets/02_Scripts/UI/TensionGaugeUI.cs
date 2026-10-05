using UnityEngine;
using UnityEngine.UI;

public class TensionGaugeUI : MonoBehaviour
{
    [Header("# Event Channel")]
    [SerializeField] private FloatEventChannel _tensionChangeEvent;

    private Slider _tensionGauge;

    private void Awake()
    {
        _tensionGauge = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _tensionChangeEvent.OnEventRaised += RefreshTensionGauge;
    }

    private void OnDisable()
    {
        _tensionChangeEvent.OnEventRaised -= RefreshTensionGauge;
    }

    private void RefreshTensionGauge(float value)
    {
        _tensionGauge.value = value;
    }

}
