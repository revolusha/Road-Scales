using UnityEngine;

public class Cargo : MonoBehaviour
{
    private GameObject _cargoStyledObject;
    private Transform _mainContainer;
    private Basket _containerBasket;

    private void OnEnable()
    {
        _mainContainer = GetComponentInParent<Transform>();

        if (_cargoStyledObject == null)
            _cargoStyledObject = StaticInstances.TryGetCurrentLevelConfig().Cargo;

        SpawnObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _containerBasket))
        {
            _containerBasket.IncreaseWeight();
            transform.parent = _containerBasket.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out _containerBasket))
        {
            _containerBasket.DecreaseWeight();
            transform.parent = _mainContainer;
        }
    }

    private void SpawnObject()
    {
        Instantiate(_cargoStyledObject, transform.position, transform.rotation, transform);
    }
}
