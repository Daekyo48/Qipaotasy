using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteJudge : MonoBehaviour
{
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

    public void RegisterActiveNote(int lane, Note note)
    {
        _activeNotes[lane].Enqueue(note);
    }

    public void Judge(int Lane)
    {
        if (_activeNotes[Lane].Count <= 0) return;

        Note targetNote = _activeNotes[Lane].Peek();
        double inputTime = GameManager.Instance.GetCurrentTime();
        double ms = Math.Abs(inputTime - targetNote.Data.JudgeTime) * 1000;

        JudgmentType result = EvaluateJudgment(ms);
        if (result != JudgmentType.None)
        {
            _activeNotes[Lane].Dequeue();
            PoolManager.Instance.Release(PoolType.Note, targetNote.gameObject);

            // 임시 로그
            Debug.Log($"{result}\nJudge Time: {targetNote.Data.JudgeTime}, Input Time: {inputTime}");
        }
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
