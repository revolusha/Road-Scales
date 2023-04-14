using UnityEngine;

public class TargetInstantFollower : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private void Update()
    {
        transform.SetPositionAndRotation(_target.transform.position, _target.transform.rotation);
    }
}