using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject _player;

    public GameObject Player => _player;

    private void OnEnable()
    {
        SpawnModel();
        SkinHandler.OnPlayerSkinChanged += SpawnModel;
    }

    private void OnDisable()
    {
        SkinHandler.OnPlayerSkinChanged -= SpawnModel;
    }

    private void SpawnModel()
    {
        if (_player != null)
            Destroy(_player);

        _player = Instantiate(Game.SkinHandler.ChoosenPlayerSkin.Object, transform);
    }
}
