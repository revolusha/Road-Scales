using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfiguration", menuName = "Road Scale Game/Level Configuration")]

public class LevelRoadConfiguration : ScriptableObject
{
    [SerializeField] private Material _roadMaterial;
    [SerializeField] private int _cargoCountPerPoint;
    [SerializeField] private RoadConfigurationPart[] _roadScheme;

    public RoadConfigurationPart[] RoadScheme => _roadScheme;
    public Material RoadMaterial => _roadMaterial;
    public int CargoCountPerPoint => Mathf.Clamp(_cargoCountPerPoint, 0, int.MaxValue);
}
