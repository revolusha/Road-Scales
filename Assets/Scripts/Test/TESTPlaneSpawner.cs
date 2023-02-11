using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPlaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _planePrefab;
    [SerializeField] private int _pathUnitLength = 100;

    private void OnEnable()
    {
        StartCoroutine(SpawnRoad());
    }

    private IEnumerator SpawnRoad()
    {
        const float TimeInterval = .1f;

        for (int z = 1; z <= _pathUnitLength; z++)
        {
            for (int x = -2; x < 3; x++)
            {
                Instantiate(
                    _planePrefab,
                    gameObject.transform.position + new Vector3(x, 0, z),
                    Quaternion.identity, transform);
            }

            yield return new WaitForSeconds(TimeInterval);
        }
    }
}
