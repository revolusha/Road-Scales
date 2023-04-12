using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScaner : MonoBehaviour
{
    private List<Obstacle> _foundObstacles = new();

    public Action OnObstacleFound;
    public Action OnNoObstacleFound;

    private bool _isFound;

    private void OnEnable()
    {
        _isFound = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            if (_foundObstacles.Contains(obstacle) == false)
                _foundObstacles.Add(obstacle);

            if (_isFound == false)
            {
                _isFound = true;
                OnObstacleFound?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            if (_foundObstacles.Contains(obstacle))
                _foundObstacles.Remove(obstacle);

            if (_foundObstacles.Count == 0)
            {
                _isFound = false;
                OnNoObstacleFound?.Invoke();
            }
        }
    }
}