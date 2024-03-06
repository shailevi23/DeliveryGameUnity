using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour, IObjectPoolManager
{
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    private List<GameObject> currentLevelObjects = new List<GameObject>();


    public void InitializeObjectPool(GameObject prefab, int initialPoolSize)
    {
        if (!objectPool.ContainsKey(prefab.tag))
        {
            objectPool[prefab.tag] = new Queue<GameObject>();
        }

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            objectPool[prefab.tag].Enqueue(obj);
        }
    }

    public void ActiveObjectFromPool(GameObject prefab, Vector3 position)
    {
        if (objectPool.ContainsKey(prefab.tag) && objectPool[prefab.tag].Count > 0)
        {
            GameObject obj = objectPool[prefab.tag].Dequeue();
            obj.transform.position = position;
            obj.SetActive(true);
            currentLevelObjects.Add(obj);
        }
        else
        {
            Debug.LogWarning("No objects available in the pool for: " + prefab.name);
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool[obj.tag].Enqueue(obj);
    }

    public void ReturnAllObjectsToPool()
    {
        foreach(GameObject obj in currentLevelObjects){
            if (obj.activeInHierarchy)
            {
                ReturnObjectToPool(obj);
            }
        }

        currentLevelObjects.Clear();
    }
}