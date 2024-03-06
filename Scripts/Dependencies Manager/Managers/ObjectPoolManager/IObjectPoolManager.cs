using UnityEngine;

public interface IObjectPoolManager
{
    void InitializeObjectPool(GameObject prefab, int initialPoolSize);
    void ActiveObjectFromPool(GameObject prefab, Vector3 position);
    void ReturnObjectToPool(GameObject obj);
    void ReturnAllObjectsToPool();
}
