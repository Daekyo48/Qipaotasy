using UnityEngine;

[System.Serializable]
public struct NoteData
{
    [SerializeField] private int _lane;
    public readonly int Lane => _lane;

    [SerializeField] private double _judgeTime;
    public readonly double JudgeTime => _judgeTime;
}
