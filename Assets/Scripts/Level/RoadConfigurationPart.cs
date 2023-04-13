using System;
using UnityEngine;

[Serializable]

public struct RoadConfigurationPart
{
    [SerializeField] private RoadType _roadType;
    [SerializeField] private bool _isGotCargo;
    [SerializeField] private int _segmentsCount;
    [SerializeField] private int _obstacleSpawnSide;

    public RoadType RoadType => _roadType;
    public bool IsNeedCargoSpawning => _isGotCargo;
    public int SegmentsCount => _segmentsCount;
    public int ObstacleSpawnSide => _obstacleSpawnSide;

    public RoadConfigurationPart(RoadType roadType = RoadType.Straight, bool isGotCargo = false, int segmentsCount = 1, int obstacleSpawnSide = 0)
    {
        _roadType = roadType;
        _isGotCargo = isGotCargo;
        _segmentsCount = segmentsCount;
        _obstacleSpawnSide = obstacleSpawnSide;
    }
}

public enum RoadType
{
    Straight,
    CurveLeft,
    CurveRight,
}