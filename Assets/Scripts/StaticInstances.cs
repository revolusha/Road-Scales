public static class StaticInstances
{
    private static int _currentLevelIndex;
    private static LevelRoadConfiguration[] _levels;

    public static int CurrentLevelIndex => _currentLevelIndex;
    public static int LevelCount => _levels.Length;
    public static bool IsLastLevel => _currentLevelIndex == _levels.Length - 1;

    public static void SetLevelsPool(LevelRoadConfiguration[] levels)
    {
        _levels = levels;
        _currentLevelIndex = 0;
    }

    public static LevelRoadConfiguration TryGetCurrentLevelConfig()
    {
        if (_levels == null)
            throw new System.Exception("Level configurations loading failed!");

        return _levels[_currentLevelIndex];
    }

    public static LevelRoadConfiguration TryGetNextLevelConfig()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex >= _levels.Length)
            _currentLevelIndex = 0;

        return TryGetCurrentLevelConfig();
    }

    public static void SwitchToNextLevel()
    {
        _currentLevelIndex++;
    }

    public static void SwitchToLevel(int index)
    {
        if (_levels == null)
            throw new System.Exception("Levels array is null!");

        if (index < 0 || index >= _levels.Length)
            throw new System.Exception("Wrong Level index!");

        _currentLevelIndex = index;
    }
}
