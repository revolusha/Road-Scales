using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _xLocalOffset = 4;
    [SerializeField] private float _yLocalOffset = -5;
    [SerializeField] private float _maximumRotationValue = 360;
    [SerializeField] private float _minimalRotationValue = 0;

    private void OnEnable()
    {
        if (_maximumRotationValue < _minimalRotationValue)
            (_maximumRotationValue, _minimalRotationValue) = (_minimalRotationValue, _maximumRotationValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Basket basket))
            basket.OnTouchedByObstacle?.Invoke();
    }

    public void SpawnObject(int side)
    {
        LevelRoadConfiguration config = StaticInstances.TryGetCurrentLevelConfig();
        float xLocalPosition;

        if (side < 0)
            xLocalPosition = -_xLocalOffset;
        else
            xLocalPosition = _xLocalOffset;

        transform.position = new(xLocalPosition, _yLocalOffset, 0);

        Vector3 rotationAmount = new (
            0, Random.Range(_minimalRotationValue, _maximumRotationValue), 0);

        Instantiate(config.Obstacle, transform.position, Quaternion.identity, transform)
            .transform.Rotate(rotationAmount);
    }
}
