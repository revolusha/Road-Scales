using UnityEngine;

public class OnFailBreaker : MonoBehaviour
{
    [SerializeField] private Vector3 _directionRangeFrom = new(-.5f, .5f, -.5f);
    [SerializeField] private Vector3 _directionRangeTo = new(.5f, 1, .5f);
    [SerializeField] private float _minimalForce = 3;
    [SerializeField] private float _maximumForce = 6;
    [SerializeField] private Behaviour[] _componentsToTurnOff;
    [SerializeField] private Collider[] _collidersToTurnOff;

    private void OnEnable()
    {
        Scales.OnScalesBroke += Break;
    }

    private void OnDisable()
    {
        Scales.OnScalesBroke -= Break;
    }

    private void Break()
    {
        DisableColliders();
        DisableComponents();

        Rigidbody rigidbody = GetRigitBody();

        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.AddForce(GetForceVector(), ForceMode.Impulse);
    }

    private Rigidbody GetRigitBody()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
            return rigidbody;
        else
            return gameObject.AddComponent<Rigidbody>();
    }

    private void DisableColliders()
    {
        for (int i = 0; i < _collidersToTurnOff.Length; i++)
        {
            _collidersToTurnOff[i].enabled = false;
        }
    }

    private void DisableComponents()
    {
        for (int i = 0; i < _componentsToTurnOff.Length; i++)
        {
            _componentsToTurnOff[i].enabled = false;
        }
    }

    private Vector3 GetForceVector()
    {
        if (_maximumForce < _minimalForce)
            (_maximumForce, _minimalForce) = (_minimalForce, _maximumForce);

        return new Vector3(
            Random.Range(_directionRangeFrom.x, _directionRangeTo.x),
            Random.Range(_directionRangeFrom.y, _directionRangeTo.y),
            Random.Range(_directionRangeFrom.z, _directionRangeTo.z))
            * Random.Range(_minimalForce, _maximumForce);
    }
}