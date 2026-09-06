using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Untitled", menuName = "Scriptable Objects/Data/MusicData")]
public class MusicData : ScriptableObject
{
    [SerializeField] private string _musicName;
    public string MusicName => _musicName;

    [SerializeField] private float _bpm;
    public float BPM => _bpm;

    [SerializeField] private double _offset;
    public double Offset => _offset;

    [SerializeField] NoteData[] _notes;
    public IReadOnlyList<NoteData> Notes => _notes;
}