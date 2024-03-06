using UnityEngine;

public interface ILevelManager
{
    LevelInfo GetCurrentLevelInfo();
    void NextLevel();
    bool IsCurrentLevelCompleted();
    int GetCurrentLevelPackagesNumber();
    int GetCurrentLevelCustomersNumber();
    int GetCurrentLevelPackageDelivered();
    void IncreaseCurrentLevelPackageDelivered();
    void ResetCurrentLevel();
    Vector3 GetCurrentLevelCarSpawnPosition();
    Quaternion GetCurrentLevelCarSpawnRotation();
    void ReturnLevelPrefab(GameObject prefab);
}
