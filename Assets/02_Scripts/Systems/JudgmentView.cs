using UnityEngine;
using System.Collections;

public class JudgmentView : MonoBehaviour
{
    [Header("# Event Channel")]
    [SerializeField] private JudgmentTypeEventChannel _judgeEvent;

    [Header("# Settings")]
    [SerializeField] GameObject _perfectObject;
    [SerializeField] GameObject _greatObject;
    [SerializeField] GameObject _goodObject;
    [SerializeField] GameObject _missObject;

    private Coroutine hideCoroutine;
    private WaitForSeconds _waitForSeconds;
    [SerializeField] private float displayTime = 0.5f;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(displayTime);
    }

    private void OnEnable()
    {
        _judgeEvent.OnEventRaised += ShowJudgment;
    }

    private void OnDisable()
    {
        _judgeEvent.OnEventRaised -= ShowJudgment;
    }

    private void ShowJudgment(JudgmentType judgment)
    {
        _perfectObject.SetActive(false);
        _greatObject.SetActive(false);
        _goodObject.SetActive(false);
        _missObject.SetActive(false);

        switch (judgment)
        {
            case JudgmentType.Perfect:
                _perfectObject.SetActive(true);
                break;

            case JudgmentType.Great:
                _greatObject.SetActive(true);
                break;

            case JudgmentType.Good:
                _greatObject.SetActive(true);
                break;

            case JudgmentType.Miss:
                _missObject.SetActive(true);
                break;
        }

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideJudgment());
    }

    private IEnumerator HideJudgment()
    {
        yield return _waitForSeconds;

        _perfectObject.SetActive(false);
        _greatObject.SetActive(false);
        _goodObject.SetActive(false);
        _missObject.SetActive(false);

        hideCoroutine = null;
    }
}
