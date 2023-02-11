using System;
using UnityEngine;

[Serializable]
public struct RoadConfigurationPart
{
    [SerializeField] private RoadType _roadType;
    [SerializeField] private int _straightLength;

    public RoadType RoadType => _roadType;
    public int StraightLength => _straightLength;
}

public enum RoadType
{
    Straight,
    CurveLeft,
    CurveRight,
}
