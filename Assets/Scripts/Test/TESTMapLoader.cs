using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTMapLoader : MonoBehaviour
{
    [SerializeField] private LevelRoadConfiguration _testLevelRoadConfiguration;

    public LevelRoadConfiguration Config => _testLevelRoadConfiguration;

    public static void Load()
    {
        Game.ISTEST = true;
        LevelReloader.ReloadLevel();
    }
}
