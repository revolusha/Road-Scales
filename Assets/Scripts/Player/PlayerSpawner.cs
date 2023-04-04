using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerDefault;

    private GameObject _player;

    public GameObject Player => _player;

    private void OnEnable()
    {
        SpawnModel(Game.SkinHandler.ChoosenPlayerSkin.Object);
        SkinHandler.OnPlayerSkinChanged += SpawnModel;
    }

    private void OnDisable()
    {
        SkinHandler.OnPlayerSkinChanged -= SpawnModel;
    }

    private void SpawnModel(GameObject skinTemplate)
    {
        if (_player != null)
            Destroy(_player);

        _player = Instantiate(skinTemplate, transform);
    }
}
