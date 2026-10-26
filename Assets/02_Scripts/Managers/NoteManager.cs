using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }

    [Header("# References")]
    [SerializeField] private JudgmentSystem _judgement;

    [Header("# Settings")]
    [SerializeField] private Transform[] _judgePoints;
    [SerializeField] private float _travelDuration = 1.5f;

    private GameManager _gameManager;
    private PoolManager _poolManager;

    private Queue<NoteData> _pendingNotes;
    private Queue<Note>[] _activeNotes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _poolManager = PoolManager.Instance;

        _pendingNotes = new Queue<NoteData>(_gameManager.MusicData.Notes);

        _activeNotes = new Queue<Note>[SettingData.LaneCount];
        for (int i = 0; i < _activeNotes.Length; i++)
        {
            _activeNotes[i] = new Queue<Note>();
        }
    }

    private void Update()
    {
        double currentTime = _gameManager.CurrentTime;

        while (_pendingNotes.TryPeek(out NoteData note) && note.JudgeTime - _travelDuration <= currentTime)
        {
            SpawnNote(_pendingNotes.Dequeue(), currentTime);
        }

        foreach (Queue<Note> notes in _activeNotes)
        {
            foreach (Note note in notes)
            {
                UpdatePosition(note, currentTime);
            }
        }
    }

    public bool TryGetNextNote(int lane, out Note note)
    {
        return _activeNotes[lane].TryPeek(out note);
    }

    public void ReleaseNote(int lane)
    {
        Note note = _activeNotes[lane].Dequeue();

        _poolManager.Release(PoolType.Note, note.gameObject);
    }

    private void SpawnNote(NoteData data, double currentTime)
    {
        Note note = _poolManager.Get(PoolType.Note).GetComponent<Note>();
        note.Initialize(data);

        UpdatePosition(note, currentTime);

        _activeNotes[data.Lane].Enqueue(note);
    }

    private void UpdatePosition(Note note, double currentTime)
    {
        Vector3 judgePosition = _judgePoints[note.Data.Lane].position;

        float remainTime = (float)(note.Data.JudgeTime - currentTime);
        float distance = _gameManager.SettingData.Speed * remainTime;

        note.transform.position = judgePosition - Vector3.down * distance;
    }
}
