public class LevelHandler
{
    private int _currentLevelIndex;
    private LevelRoadConfiguration[] _levels;

    public int CurrentLevelIndex => _currentLevelIndex;
    public int LevelCount => _levels.Length;
    public bool IsLastLevel => _currentLevelIndex == _levels.Length - 1;

    public LevelHandler()
    {
        _currentLevelIndex = 0;
    }

    public void SetLevelsPool(LevelRoadConfiguration[] levels)
    {
        _levels = levels;
    }

    public LevelRoadConfiguration TryGetCurrentLevelConfig()
    {
        if (Game.ISTEST == true)
            return Game.TEST.Config;

        if (_levels == null)
            throw new System.Exception("Level configurations loading failed!");

        return _levels[_currentLevelIndex];
    }

    public LevelRoadConfiguration TryGetNextLevelConfig()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex >= _levels.Length)
            _currentLevelIndex = 0;

        return TryGetCurrentLevelConfig();
    }

    public void SwitchToNextLevel()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex >= _levels.Length)
            _currentLevelIndex = 0;
    }

    public void SwitchToLevel(int index)
    {
        if (_levels == null)
            throw new System.Exception("Levels array is null!");

        if (index < 0 || index >= _levels.Length)
            throw new System.Exception("Wrong Level index!");

        _currentLevelIndex = index;
    }
}
