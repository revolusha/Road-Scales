using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class ObstacleIndicator : MonoBehaviour
{
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;

    private SphereCollider _collider;

    private void OnEnable()
    {
        _collider = GetComponent<SphereCollider>();
        OrderRadiuses();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new(0, 0, 0, .3f);
        Gizmos.DrawSphere(transform.position, _minRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxRadius);
    }

    private void OrderRadiuses()
    {
        if (_minRadius > _maxRadius)
            (_maxRadius, _minRadius) = (_minRadius, _maxRadius);
    }
}