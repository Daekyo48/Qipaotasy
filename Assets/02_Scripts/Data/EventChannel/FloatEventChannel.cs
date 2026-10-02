using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Scriptable Objects/EventChannel/FloatEventChannel")]
public class FlaotEventChannel : ScriptableObject
{
    public event Action<float> OnEventRaised;

    public void Raise(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}
