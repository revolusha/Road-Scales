using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadSegment))]

public class RoadBuilder : MonoBehaviour
{
    [SerializeField] private LevelRoadConfiguration _configuration;
    [SerializeField] private GameObject _finishRoadSegmentTemplate;
    [SerializeField] private GameObject _straightRoadSegmentTemplate;
    [SerializeField] private GameObject _curveLeftRoadSegmentTemplate;
    [SerializeField] private GameObject _curveRightRoadSegmentTemplate;
    [SerializeField] private GameObject _cargoTemplate;

    private const int MaxRoadPartSegmentLength = 10;
    private const int SpawnCargoSegmentPeriod = 2;
    private const int FirstSpawnCargoSegmentIndex = 1;

    private RoadSegment _startSegment;
    private RoadSegment _currentSegment;
    private List<CargoSpawner> _cargoSpawners;

    public static Action OnRoadReady;

    private void OnEnable()
    {
        _cargoSpawners = new List<CargoSpawner>();
        _startSegment = GetComponent<RoadSegment>();
        _currentSegment = _startSegment;
    }

    private void Start()
    {
        _startSegment.SetMaterial(_configuration.RoadMaterial);
        Build();
        SpawnAllCargo();
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

        Instantiate(_finishRoadSegmentTemplate, transform).GetComponent<RoadSegment>()
            .PutBehindSegment(_currentSegment)
            .SetMaterial(_configuration.RoadMaterial);
    }

    private RoadSegment[] CreateRoadPart(RoadConfigurationPart partConfiguration)
    {
        switch (partConfiguration.RoadType)
        {
            case RoadType.Straight:
                return CreateSegments(_straightRoadSegmentTemplate, partConfiguration.StraightLength);

            case RoadType.CurveLeft:
                return CreateSegments(_curveLeftRoadSegmentTemplate);

            case RoadType.CurveRight:
                return CreateSegments(_curveRightRoadSegmentTemplate);

            default: 
                throw new ArgumentOutOfRangeException(nameof(partConfiguration.RoadType), "Invalid type!");
        }
    }

    private RoadSegment[] CreateSegments(GameObject template, int length = 1)
    {
        length = Mathf.Clamp(length, 0, MaxRoadPartSegmentLength);

        RoadSegment[] newSegments = new RoadSegment[length];

        for (int i = 0; i < length; i++)
        {
            newSegments[i] = Instantiate(template, transform).GetComponent<RoadSegment>();

            if (i % SpawnCargoSegmentPeriod == FirstSpawnCargoSegmentIndex)
            {
                CargoSpawner spawner = newSegments[i].gameObject.AddComponent<CargoSpawner>();

                _cargoSpawners.Add(spawner);
            }
        }

        return newSegments;
    }

    private void SpawnAllCargo()
    {
        foreach (var spawner in _cargoSpawners)
            spawner.SpawnCargo(_cargoTemplate, _configuration.CargoCountPerPoint);
    }
}