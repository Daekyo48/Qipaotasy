using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("# Data")]
    [SerializeField] private PoolData[] _poolData;

    [Header("# Settings")]
    [SerializeField] private Transform _rootObject;

    private Dictionary<PoolType, Queue<GameObject>> _pools;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _pools = new Dictionary<PoolType, Queue<GameObject>>(_poolData.Length);
    }

    private void Start()
    {
        CreatePools();
    }

    private void CreatePools()
    {
        foreach (PoolData data in _poolData)
        {
            _pools[data.Type] = new Queue<GameObject>(data.Count);

            for (int i = 0; i < data.Count; i++)
            {
                GameObject poolObject = Instantiate(data.Prefab, _rootObject);
                
                poolObject.SetActive(false);
                _pools[data.Type].Enqueue(poolObject);
            }
        }
    }

    public GameObject Get(PoolType type)
    {
        if (_pools[type].TryDequeue(out GameObject selectedObject))
        {
            selectedObject.SetActive(true);
        }
        else
        {
            selectedObject = Instantiate(_poolData[(int)type].Prefab, _rootObject);
        }

        return selectedObject;
    }

    public void Release(PoolType type, GameObject targetObject)
    {
        targetObject.SetActive(false);
        _pools[type].Enqueue(targetObject);
    }
}
