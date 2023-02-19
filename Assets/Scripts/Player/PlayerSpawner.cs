using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerTemplate;

    private Player _player;

    private void OnEnable()
    {
        _player = Instantiate(_playerTemplate, transform).GetComponent<Player>();
    }

    private void OnDrawGizmos()
    {
        const float Radius = .5f;

        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, Radius);
    }
}
