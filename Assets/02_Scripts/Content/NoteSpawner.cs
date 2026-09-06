using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("# References")]
    [SerializeField] private NoteJudge _judge;

    [Header("# Settings")]
    [SerializeField] private Transform[] _startPoints;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private float _travelDuration = 1.5f;

    private Queue<NoteData> _notes;

    public void Awake()
    {
        _notes = new Queue<NoteData>(GameManager.Instance.MusicData.Notes);
    }

    public void Update()
    {
        double currentTime = GameManager.Instance.GetCurrentTime();

        while (_notes.Count > 0 && _notes.Peek().JudgeTime - _travelDuration <= currentTime)
        {
            SpawnNote(_notes.Dequeue());
        }
    }

    private void SpawnNote(NoteData data)
    {
        Vector3 startPoint = _startPoints[data.Lane].position;
        Vector3 targetPoint = _targetPoints[data.Lane].position;

        GameObject noteObject = PoolManager.Instance.Get(PoolType.Note);
        Note note = noteObject.GetComponent<Note>();

        note.Initialize(data, startPoint, targetPoint, _travelDuration);
        _judge.RegisterActiveNote(data.Lane, note);
    }
}
