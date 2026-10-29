using UnityEngine;

public class ObstructionSystem : MonoBehaviour
{
    [SerializeField] private float _transitionDuration = 2f;

    private readonly double[] _triggerTimes = { 8f, 16f, 24f, 32f };
    private readonly float[] _multipliers = { 1.5f, 1f, 0.5f, 1f };

    private int _currentIndex = 0;
    private bool _isTransitioning = false;

    private float _startMultiplier;
    private float _targetMultiplier;
    private double _transitionStartTime;

    // 언젠간 개선하기
    private void Update()
    {
        double currentTime = GameManager.Instance.CurrentTime;

        if (!_isTransitioning && _currentIndex < _triggerTimes.Length && currentTime >= _triggerTimes[_currentIndex])
        {
            _startMultiplier = GameManager.Instance.SettingData.Multiplier;
            _targetMultiplier = _multipliers[_currentIndex];
            _transitionStartTime = currentTime;

            _isTransitioning = true;
            _currentIndex++;
        }

        if (!_isTransitioning) return;

        float elapsed = (float)(currentTime - _transitionStartTime);
        float t = Mathf.Clamp01(elapsed / _transitionDuration);

        GameManager.Instance.SettingData.Multiplier = Mathf.Lerp(_startMultiplier, _targetMultiplier, t);

        if (t >= 1f)
        {
            GameManager.Instance.SettingData.Multiplier = _targetMultiplier;
            _isTransitioning = false;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.SettingData.Multiplier = 1f;
    }
}
