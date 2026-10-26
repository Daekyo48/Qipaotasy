using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "Scriptable Objects/Data/SettingData")]
public class SettingData : ScriptableObject
{
    public const int LaneCount = 4;

    [SerializeField] private float _speed;
    public float Speed
    {
        get => _speed;
        set => Mathf.Clamp(_speed, 1f, 9f);
    }
}
