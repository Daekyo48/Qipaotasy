using System;
using UnityEngine;

public class JudgmentSystem : MonoBehaviour
{
    [Header("# Event Channel")]
    [SerializeField] private JudgmentTypeEventChannel _judgeEvent;

    [Header("# Settings")]
    [SerializeField] private int _perfectRange = 30;
    [SerializeField] private int _greatRange = 60;
    [SerializeField] private int _goodRange = 100;
    [SerializeField] private int _missRange = 150;

    private GameManager _gameManager;
    private NoteManager _noteManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _noteManager = NoteManager.Instance;
    }

    private void OnEnable()
    {
        InputManager.Instance.LaneJudgmentInput += OnLaneJudgmentInput;
    }

    private void OnDisable()
    {
        InputManager.Instance.LaneJudgmentInput -= OnLaneJudgmentInput;
    }

    private void Update()
    {
        CheckMissedNotes();
    }

    private void OnLaneJudgmentInput(int lane)
    {
        if (!_noteManager.TryGetNextNote(lane, out Note nextNote)) return;

        double inputTime = _gameManager.CurrentTime;
        double ms = Math.Abs(inputTime - nextNote.Data.JudgeTime) * 1000;

        JudgmentType judgment = EvaluateJudgment(ms);
        if (judgment != JudgmentType.None)
        {
            CompleteJudgment(lane, judgment);

            // 임시 로그
            Debug.Log($"{judgment}\nJudge Time: {nextNote.Data.JudgeTime}, Input Time: {inputTime}");
        }
    }

    private void CheckMissedNotes()
    {
        double currentTime = _gameManager.CurrentTime;

        for (int i = 0; i < SettingData.LaneCount; i++)
        {
            while (_noteManager.TryGetNextNote(i, out Note nextNote) && (currentTime - nextNote.Data.JudgeTime) * 1000 > _missRange * 5)
            {
                CompleteJudgment(i, JudgmentType.Miss);
            }
        }
    }

    private void CompleteJudgment(int lane, JudgmentType judgment)
    {
        _noteManager.DespawnNote(lane);
        _judgeEvent.Raise(judgment);
    }

    private JudgmentType EvaluateJudgment(double ms)
    {
        if (ms <= _perfectRange) return JudgmentType.Perfect;
        if (ms <= _greatRange) return JudgmentType.Great;
        if (ms <= _goodRange) return JudgmentType.Good;
        if (ms <= _missRange) return JudgmentType.Miss;
        
        return JudgmentType.None;
    }
}
