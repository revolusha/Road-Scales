using System.Collections.Generic;
using UnityEngine;

public class CargoSpawner : MonoBehaviour
{
    private float _spawnAreaRadius = 2.2f;
    private float _minimalCargoDistance = .3f;
    private float _cargoSpawnHeight = .16f;

    private CargoSpawnPoint _cargoSpawnPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color( 0, 0, 1, .2f );
        Gizmos.DrawSphere(GetComponentInChildren<CargoSpawnPoint>().transform.position, _spawnAreaRadius);
    }

    private void OnEnable()
    {
        _cargoSpawnPoint = GetComponentInParent<RoadSegment>().GetComponentInChildren<CargoSpawnPoint>();
    }

    public void SpawnCargo(GameObject cargoTemplate, int count)
    {
        Vector3[] localSpawnPoints = GetSpawnLocalPositions(count);

        for (int i = 0; i < localSpawnPoints.Length; i++)
        {
            Vector3 newPosition = new(
                localSpawnPoints[i].x + _cargoSpawnPoint.transform.position.x,
                _cargoSpawnHeight, 
                localSpawnPoints[i].z + _cargoSpawnPoint.transform.position.z);

            Instantiate(cargoTemplate, newPosition, GetRandomRotation(), CargoContainer.Transform);
        }
    }

    private Vector3[] GetSpawnLocalPositions(int count)
    {
        List<Vector2> spawnPositions = GeneratePositionsList(count);

        Vector3[] positions = new Vector3[spawnPositions.Count];

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            positions[i] = new Vector3(spawnPositions[i].x, 0, spawnPositions[i].y);
        }

        return positions;
    }

    private List<Vector2> GeneratePositionsList(int count)
    {
        List<Vector2> list = new();

        list.Add(GenerateNewPosition());

        for (int i = 0; i < count; ++i)
        {
            if (TryGetPosition(out Vector2 position, list))
                list.Add(position);
        }

        return list;
    }

    private bool TryGetPosition(out Vector2 position, List<Vector2> listToCheck)
    {
        const int MaxTryCount = 30;

        bool isFound = false;

        position = new Vector2(0, 0);

        for (int t = 0; t < MaxTryCount; t++)
        {
            position = GenerateNewPosition();
            isFound = true;

            foreach (Vector2 point in listToCheck)
            {
                if (Vector2.Distance(point, position) < _minimalCargoDistance)
                {
                    isFound = false;
                    break;
                }
            }

            if (isFound)
                break;
        }

        return isFound;
    }

    private Vector2 GenerateNewPosition()
    {
        const float MaxDegrees = 360;

        float degrees = Random.Range(0, MaxDegrees);
        float radius = Random.Range(0, _spawnAreaRadius);

        Vector2 position = new(Mathf.Cos(degrees), Mathf.Sin(degrees));

        return new Vector2(position.x * radius, position.y * radius);
    }

    private Quaternion GetRandomRotation()
    {
        const float MaxRotation = 1;

        return new Quaternion(0, Random.Range(0, MaxRotation), 0, 1);
    }
}
