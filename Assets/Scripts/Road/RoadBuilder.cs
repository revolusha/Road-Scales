using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadSegment))]

public class RoadBuilder : MonoBehaviour
{
    [SerializeField] private RoadSegment _finishRoadSegmentOnScene;
    [SerializeField] private GameObject _straightRoadSegmentTemplate;
    [SerializeField] private GameObject _curveLeftRoadSegmentTemplate;
    [SerializeField] private GameObject _curveRightRoadSegmentTemplate;
    [SerializeField] private GameObject _cargoBaseTemplate;
    [SerializeField] private GameObject _obstacleBaseTemplate;

    private const int MaxRoadPartSegmentLength = 20;

    private LevelRoadConfiguration _configuration;
    private RoadSegment _startSegment;
    private RoadSegment _currentSegment;
    private List<CargoSpawner> _cargoSpawners;

    public static Action OnRoadReady;

    public static bool IsReady { get; private set; }

    private void OnEnable()
    {
        IsReady = false;
        _configuration = StaticInstances.TryGetCurrentLevelConfig();
        _cargoSpawners = new List<CargoSpawner>();
        _startSegment = GetComponent<RoadSegment>();
        _currentSegment = _startSegment;
    }

    private void Start()
    {
        _startSegment.SetMaterial(_configuration.RoadMaterial);
        Build();
        SpawnAllCargo();
        IsReady = true;
        OnRoadReady?.Invoke();

    }

    private void Build()
    {
        foreach(RoadConfigurationPart partConfiguration in _configuration.RoadScheme)
        {
            RoadSegment[] part = CreateRoadPart(partConfiguration);

            for(int i = 0; i < part.Length; i++)
            {
                part[i].PutBehindSegment(_currentSegment).SetMaterial(_configuration.RoadMaterial);
                _currentSegment = part[i];
            }
        }

        _finishRoadSegmentOnScene
            .PutBehindSegment(_currentSegment)
            .SetMaterial(_configuration.RoadMaterial);
    }

    private RoadSegment[] CreateRoadPart(RoadConfigurationPart partConfiguration)
    {
        return partConfiguration.RoadType switch
        {
            RoadType.Straight => CreateSegments(_straightRoadSegmentTemplate, partConfiguration),
            RoadType.CurveLeft => CreateSegments(_curveLeftRoadSegmentTemplate, partConfiguration),
            RoadType.CurveRight => CreateSegments(_curveRightRoadSegmentTemplate, partConfiguration),
            _ => throw new ArgumentOutOfRangeException(nameof(partConfiguration.RoadType), "Invalid type!"),
        };
    }

    private RoadSegment[] CreateSegments(GameObject template, RoadConfigurationPart partConfiguration)
    {
        float length = Mathf.Clamp(partConfiguration.SegmentsCount, 0, MaxRoadPartSegmentLength);

        RoadSegment[] newSegments = new RoadSegment[partConfiguration.SegmentsCount];

        for (int i = 0; i < length; i++)
        {
            newSegments[i] = Instantiate(template, transform).GetComponent<RoadSegment>();

            if (partConfiguration.IsNeedCargoSpawning)
                _cargoSpawners.Add(newSegments[i].gameObject.AddComponent<CargoSpawner>());

            if (partConfiguration.ObstacleSpawnSide != 0)
                Instantiate(
                    _obstacleBaseTemplate, 
                    newSegments[i].transform.position, 
                    newSegments[i].transform.rotation,
                    newSegments[i].transform)
                    .GetComponent<Obstacle>().SpawnObject(partConfiguration.ObstacleSpawnSide);
        }

        return newSegments;
    }

    private void SpawnAllCargo()
    {
        foreach (var spawner in _cargoSpawners)
            spawner.SpawnCargo(_cargoBaseTemplate, _configuration.CargoCountPerPoint);
    }
}