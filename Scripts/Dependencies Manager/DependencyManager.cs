using System.Collections.Generic;
using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    public static DependencyManager Instance { get; private set; }

    public ISpeedAdjuster SpeedAdjuster { get; private set; }
    public ICollisionHandler CollisionHandler { get; private set; }
    public ITriggerHandler TriggerHandler { get; private set; }
    public IDeliveryAnimation DeliveryAnimation { get; private set; }
    public IInGameUI InGameUI { get; private set; }
    public ISettingsUI SettingsUI { get; private set; }
    public IChangeCar ChangeCar { get; private set; }
    public ILevelManager LevelManager { get; private set; }
    public IPrefabManager PrefabManager { get; private set; }
    public ILevelConfigManager LevelConfigManager { get; private set; }
    public IObjectPoolManager ObjectPoolManager { get; private set; }
    public IHealthSystem HealthSystem { get; private set; }
    public ILevelIntro LevelIntro { get; private set; }
    public ITimer Timer { get; private set; }
    public IRestartable Restartable { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SpeedAdjuster = GetComponentInChildren<SpeedAdjuster>();
        CollisionHandler = GetComponentInChildren<CollisionHandler>();
        TriggerHandler = GetComponentInChildren<TriggerHandler>();
        DeliveryAnimation = GetComponentInChildren<DeliveryAnimation>();
        InGameUI = GetComponentInChildren<InGameUI>();
        LevelIntro = GetComponentInChildren<InGameUI>().GetComponentInChildren<LevelIntro>();
        HealthSystem = GetComponentInChildren<InGameUI>().GetComponentInChildren<HealthSystem>();
        Timer = GetComponentInChildren<InGameUI>().GetComponentInChildren<Timer>();
        Restartable = GetComponentInChildren<InGameUI>().GetComponentInChildren<Restartable>();
        SettingsUI = GetComponentInChildren<SettingsUI>();
        ChangeCar = GetComponentInChildren<SettingsUI>().GetComponentInChildren<ChangeCar>();
        LevelManager = GetComponentInChildren<LevelManager>();
        PrefabManager = GetComponentInChildren<PrefabManager>();
        LevelConfigManager = GetComponentInChildren<LevelManager>().GetComponentInChildren<LevelConfigManager>();
        ObjectPoolManager = GetComponentInChildren<ObjectPoolManager>();

        CheckDependencies(new List<object>{ SpeedAdjuster, CollisionHandler, TriggerHandler, DeliveryAnimation,
            InGameUI, SettingsUI, ChangeCar, LevelManager, PrefabManager, LevelConfigManager, ObjectPoolManager,
            HealthSystem, LevelIntro, Timer, Restartable });
    }

    private void CheckDependencies(List<object> dependencies)
    {
        foreach(var dependency in dependencies)
        {
            if (dependency == null)
            {
                Debug.LogError(dependency.GetType() + " component not found on GameObject: " + gameObject.name);
            }
        }
    }
}