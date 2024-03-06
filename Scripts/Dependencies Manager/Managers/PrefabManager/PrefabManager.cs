using System;
using UnityEngine;

public class PrefabManager : MonoBehaviour, IPrefabManager
{
    public IObjectPoolManager _objectPoolManager;

    [SerializeField]
    public GameObject[] prefabs;

    void Start()
    {
        _objectPoolManager = DependencyManager.Instance.ObjectPoolManager;
        InitializeObjectPools();
    }

    public void InitializeObjectPools()
    {
        foreach (GameObject prefab in prefabs)
        {
            _objectPoolManager.InitializeObjectPool(prefab, 20);
        }
    }

    public void ReturnUsedPrefab(GameObject prefab)
    {
        _objectPoolManager.ReturnObjectToPool(prefab);
    }

    public void ActivateLevelPrefabs(LevelInfo levelInfo)
    {
        if (levelInfo == null)
        {
            Debug.LogError("LevelInfo is null. Cannot instantiate prefabs.");
            return;
        }

        try
        {
            foreach(GameObject prefab in prefabs)
            {
                InstantiatePrefabsOfType(prefab, GetPrefabSize(levelInfo, prefab), GetPrefabsSpawnPositions(levelInfo, prefab));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while instantiating prefabs: {ex.Message}");
        }
    }

    public void ReturnAllPrefabsToPool()
    {
        _objectPoolManager.ReturnAllObjectsToPool();
    }

    private Vector3[] GetPrefabsSpawnPositions(LevelInfo levelInfo, GameObject prefab)
    {
        switch (prefab.tag)
        {
            case "Package":
                return levelInfo.packageSpawnPositions;
            case "Customer":
                return levelInfo.customerSpawnPositions;
            case "Boost":
                return levelInfo.boostSpawnPositions;
            default:
                throw new ArgumentException("Invalid prefab tag: " + prefab.tag);
        }
    }

    private int GetPrefabSize(LevelInfo levelInfo, GameObject prefab)
    {
        switch (prefab.tag)
        {
            case "Package":
                return levelInfo.packages;
            case "Customer":
                return levelInfo.customers;
            case "Boost":
                return levelInfo.boosts;
            default:
                throw new ArgumentException("Invalid prefab tag: " + prefab.tag);
        }
    }

    private void InstantiatePrefabsOfType(GameObject prefab, int count, Vector3[] spawnPositions)
    {
        for (int i = 0; i < count; i++)
        {
            try
            {
                _objectPoolManager.ActiveObjectFromPool(prefab, spawnPositions[i]);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error while instantiating {prefab} prefab: {ex.Message}");
            }
        }
    }
}
