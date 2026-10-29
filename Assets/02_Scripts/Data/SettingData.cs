using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "Scriptable Objects/Data/SettingData")]
public class SettingData : ScriptableObject
{
    public const int LaneCount = 4;

    [SerializeField] private float _userSpeed;
    public float UserSpeed
    {
        get => _userSpeed;
        set => _userSpeed = Mathf.Clamp(value, 2f, 18f);
    }

    [SerializeField] private float _multiplier;
    public float Multiplier
    {
        get => _multiplier;
        set => _multiplier = value;
    }

    public float Speed
    {
        get => _userSpeed * _multiplier;
    }
}
