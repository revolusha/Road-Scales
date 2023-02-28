using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerTemplate;

    private PlayerCharacterMoving _player;

    private void OnEnable()
    {
        _player = Instantiate(_playerTemplate, transform).GetComponent<PlayerCharacterMoving>();
    }
}
