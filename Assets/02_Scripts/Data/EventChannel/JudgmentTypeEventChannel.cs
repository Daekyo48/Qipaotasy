using System;
using UnityEngine;

[CreateAssetMenu(fileName = "JudgmentTypeEventChannel", menuName = "Scriptable Objects/EventChannel/JudgmentTypeEventChannel")]
public class JudgmentTypeEventChannel : ScriptableObject
{
    public event Action<JudgmentType> OnEventRaised;

    public void Raise(JudgmentType value)
    {
        OnEventRaised?.Invoke(value);
    }
}
