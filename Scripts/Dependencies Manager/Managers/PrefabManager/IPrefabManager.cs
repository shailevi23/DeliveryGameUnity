using UnityEngine;

public interface IPrefabManager
{
    void InitializeObjectPools();
    void ActivateLevelPrefabs(LevelInfo levelInfo);
    void ReturnUsedPrefab(GameObject prefab);
    void ReturnAllPrefabsToPool();
}

