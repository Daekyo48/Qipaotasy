using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "Scriptable Objects/Data/SettingData")]
public class SettingData : ScriptableObject
{
    public const int LaneCount = 4;

    [SerializeField] private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = Mathf.Clamp(value, 2f, 18f);
    }
}
