using UnityEngine;

public class Cargo : MonoBehaviour
{
    private Transform _mainContainer;
    private Basket _containerBasket;
    private GameObject _model;

    private void OnEnable()
    {
        _mainContainer = GetComponentInParent<Transform>();
        SpawnModel();

        SkinHandler.OnCargoSkinChanged += SpawnModel;
    }

    private void OnDisable()
    {
        SkinHandler.OnCargoSkinChanged -= SpawnModel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _containerBasket))
        {
            _containerBasket.UpdateWeight();
            transform.parent = _containerBasket.transform;
            Game.SoundPlayer.PlayCargoDropSound();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out _containerBasket))
        {
            _containerBasket.UpdateWeight();
            transform.parent = _mainContainer;
        }
    }

    private void SpawnModel()
    {
        if (_model != null)
            Destroy(_model);

        _model = Instantiate(Game.SkinHandler.ChoosenCargoSkin.Object, transform.position, transform.rotation, transform);
    }
}
