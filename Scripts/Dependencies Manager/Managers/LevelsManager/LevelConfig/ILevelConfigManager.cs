using System.Collections.Generic;

public interface ILevelConfigManager
{
    Dictionary<int, LevelInfo> GetLevelConfig();
}
