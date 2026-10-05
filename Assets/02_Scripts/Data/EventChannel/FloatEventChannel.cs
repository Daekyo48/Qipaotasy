using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Scriptable Objects/EventChannel/FloatEventChannel")]
public class FloatEventChannel : ScriptableObject
{
    public event Action<float> OnEventRaised;

    public void Raise(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}
