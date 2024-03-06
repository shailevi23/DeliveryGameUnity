using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager
{
    private IPrefabManager _prefabManager;
    private IHealthSystem _healthSystem;

    private Dictionary<int, LevelInfo> levels;
    private int currentLevelIndex = 1;
    private int currentLevelPackagesDelivered = 0;

    private void Start()
    {
        levels = DependencyManager.Instance.LevelConfigManager.GetLevelConfig();

        _prefabManager = DependencyManager.Instance.PrefabManager;
        _healthSystem = DependencyManager.Instance.HealthSystem;

        ActivateCurrentLevelPrefabs();

        _healthSystem.OnHealthChanged += IsHealthBarEmpty;
    }

    private void Update()
    {
        if (IsCurrentLevelCompleted() && GameManager.Instance.CurrentState.Equals(GameState.Gameplay))
        {
            GameManager.Instance.SetGameState(GameState.LevelEndedSuccesfully);
            // use NextLevel() as a button listener;
        }
    }

    public void NextLevel()
    {
        GameManager.Instance.SetGameState(GameState.LevelEndedSuccesfully);
        currentLevelPackagesDelivered = 0;
        _prefabManager.ReturnAllPrefabsToPool();
        currentLevelIndex++;
        ActivateCurrentLevelPrefabs();
        GameManager.Instance.SetGameState(GameState.LevelIntro);
    }

    public LevelInfo GetCurrentLevelInfo()
    {
        return levels[currentLevelIndex];
    }

    public bool IsCurrentLevelCompleted()
    {
        return currentLevelPackagesDelivered == levels[currentLevelIndex].packages;
    }

    public int GetCurrentLevelPackagesNumber()
    {
        return levels[currentLevelIndex].packages;
    }

    public int GetCurrentLevelCustomersNumber()
    {
        return levels[currentLevelIndex].customers;
    }

    public int GetCurrentLevelPackageDelivered()
    {
        return currentLevelPackagesDelivered;
    }

    public void IncreaseCurrentLevelPackageDelivered()
    {
        currentLevelPackagesDelivered++;
    }

    public void ResetCurrentLevel()
    {
        if(GameManager.Instance.CurrentState != GameState.LevelIntro)
        {
            currentLevelPackagesDelivered = 0;
            _prefabManager.ReturnAllPrefabsToPool();
            ActivateCurrentLevelPrefabs();
            GameManager.Instance.SetGameState(GameState.LevelIntro);
        }
    }

    public Vector3 GetCurrentLevelCarSpawnPosition()
    {
        return levels[currentLevelIndex].carSpawnPosition;
    }

    public Quaternion GetCurrentLevelCarSpawnRotation()
    {
        return levels[currentLevelIndex].carSpawnRotation;
    }

    public void ReturnLevelPrefab(GameObject prefab)
    {
        _prefabManager.ReturnUsedPrefab(prefab);
    }

    private void IsHealthBarEmpty(float health)
    {
        if(health == 0 && !IsCurrentLevelCompleted() && GameManager.Instance.CurrentState.Equals(GameState.Gameplay))
        {
            GameManager.Instance.SetGameState(GameState.LevelEndedWithGameOver);
        }
    }

    private void ActivateCurrentLevelPrefabs()
    {
        _prefabManager.ActivateLevelPrefabs(GetCurrentLevelInfo());
    }
}