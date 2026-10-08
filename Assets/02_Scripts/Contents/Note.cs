using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public event Action<int, JudgmentType> MissEvent;

    public NoteData Data { get; private set; }
    public bool IsJudged { get; set; }

    private Vector3 _spawnPosition;
    private Vector3 _judgePosition;
    private double _travelDuration;
    private double _spawnTime;

    public void Initialize(NoteData data, Vector3 spawnPosition, Vector3 judgePosition, double travelDuration)
    {
        Data = data;
        _spawnPosition = spawnPosition;
        _judgePosition = judgePosition;
        _travelDuration = travelDuration;
        _spawnTime = data.JudgeTime - travelDuration;
    }

    private void Update()
    {
        double elapsed = GameManager.Instance.GetCurrentTime() - _spawnTime;
        float progress = (float)(elapsed / _travelDuration);

        transform.position = Vector3.LerpUnclamped(_spawnPosition, _judgePosition, progress);
    }

    private void OnBecameInvisible()
    {
       if (IsJudged) return;

       MissEvent.Invoke(Data.Lane, JudgmentType.Miss);
    }
}
