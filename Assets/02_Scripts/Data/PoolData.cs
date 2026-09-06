using UnityEngine;

[System.Serializable]
public struct PoolData
{
    [SerializeField] private PoolType _type;
    public readonly PoolType Type => _type;

    [SerializeField] private GameObject _prefab;
    public readonly GameObject Prefab => _prefab;

    [SerializeField] private int _count;
    public readonly int Count => _count;
}