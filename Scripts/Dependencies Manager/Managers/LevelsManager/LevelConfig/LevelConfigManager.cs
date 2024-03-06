using UnityEngine;
using System.Collections.Generic;

public class LevelConfigManager : MonoBehaviour, ILevelConfigManager
{
    [SerializeField]
    public List<LevelInfo> levelInfos = new List<LevelInfo>();

    public Dictionary<int, LevelInfo> GetLevelConfig()
    {
        Dictionary<int, LevelInfo> levelConfigs = new Dictionary<int, LevelInfo>();

        foreach (LevelInfo levelInfo in levelInfos)
        {
            levelConfigs.Add(levelInfo.levelNumber, levelInfo);
        }

        return levelConfigs;
    }
}