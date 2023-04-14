using System.Collections.Generic;
using UnityEngine;

public class ObstacleIndicator : MonoBehaviour
{
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;

    private float _gap;
    private readonly List<ObstacleSign> _obstacleSigns = new();

    private void OnEnable()
    {
        OrderRadiuses();
        _gap = _maxRadius - _minRadius;
    }

    private void Update()
    {
        foreach(ObstacleSign sign in _obstacleSigns)
            UpdateSign(sign);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObstacleSign obstacleSign))
            _obstacleSigns.Add(obstacleSign);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ObstacleSign obstacleSign))
        {
            obstacleSign.ColorUpdate(0);
            _obstacleSigns.Remove(obstacleSign);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new(0, 0, 0, .3f);
        Gizmos.DrawSphere(transform.position, _minRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxRadius);
    }

    private void UpdateSign(ObstacleSign sign)
    {
        const float MaxTransparencyValue = 1;

        float distance = Vector3.Distance(transform.position, sign.transform.position);

        if (distance <= _minRadius)
            sign.ColorUpdate(MaxTransparencyValue);
        else if (distance >= _maxRadius)
            sign.ColorUpdate(0);
        else
            sign.ColorUpdate((_maxRadius - distance) / _gap);
    }

    private void OrderRadiuses()
    {
        if (_minRadius > _maxRadius)
            (_maxRadius, _minRadius) = (_minRadius, _maxRadius);
    }
}