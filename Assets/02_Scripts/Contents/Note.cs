using UnityEngine;

public class Note : MonoBehaviour
{
    public NoteData Data { get; private set; }

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
}
