using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfiguration", menuName = "Road Scale Game/Level Configuration")]

public class LevelRoadConfiguration : ScriptableObject
{
    [SerializeField] private Material _roadMaterial;
    [SerializeField] private int _cargoCountPerPoint;
    [SerializeField] private GameObject _obstacleStyledTemplate;
    [SerializeField] private GameObject _cargoStyledTemplate;
    [SerializeField] private RoadConfigurationPart[] _roadScheme = {
        new RoadConfigurationPart(segmentsCount: 1), 
        new RoadConfigurationPart(segmentsCount: 1, isGotCargo: true), 
        new RoadConfigurationPart(segmentsCount: 1) };

    public RoadConfigurationPart[] RoadScheme => _roadScheme;
    public Material RoadMaterial => _roadMaterial;
    public int CargoCountPerPoint => Mathf.Clamp(_cargoCountPerPoint, 0, int.MaxValue);
    public GameObject Obstacle => _obstacleStyledTemplate;
    public GameObject Cargo => _cargoStyledTemplate;
}
