using System;
using System.Collections.Generic;
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

    private readonly Queue<Note>[] _activeNotes = new Queue<Note>[4];

    private void Awake()
    {
        for (int i = 0; i < _activeNotes.Length; i++)
        {
            _activeNotes[i] = new Queue<Note>();
        }
    }

    private void OnEnable()
    {
        InputManager.Instance.JudgeInputEvent += TryJudge;
    }

    private void OnDisable()
    {
        InputManager.Instance.JudgeInputEvent -= TryJudge;
    }

    public void RegisterActiveNote(int lane, Note note)
    {
        note.MissEvent += ProcessJudgment;

        _activeNotes[lane].Enqueue(note);
    }

    private void TryJudge(int lane)
    {
        if (_activeNotes[lane].Count == 0) return;

        Note targetNote = _activeNotes[lane].Peek();
        double inputTime = GameManager.Instance.GetCurrentTime();
        double ms = Math.Abs(inputTime - targetNote.Data.JudgeTime) * 1000;

        JudgmentType judgment = EvaluateJudgment(ms);
        if (judgment != JudgmentType.None)
        {
            targetNote.IsJudged = true;

            ProcessJudgment(lane, judgment);

            // 임시 로그
            Debug.Log($"{judgment}\nJudge Time: {targetNote.Data.JudgeTime}, Input Time: {inputTime}");
        }
    }

    // 이 저주받은 구조는 조만간 손을 볼 것.
    private void ProcessJudgment(int lane, JudgmentType judgment)
    {
        Note note = _activeNotes[lane].Peek();

        note.MissEvent -= ProcessJudgment;
        PoolManager.Instance.Release(PoolType.Note, note.gameObject);

        _activeNotes[lane].Dequeue();
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
