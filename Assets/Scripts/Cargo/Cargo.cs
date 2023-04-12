using UnityEngine;

public class Cargo : MonoBehaviour
{
    [SerializeField] private GameObject _defaultSkin;

    private Transform _mainContainer;
    private Basket _containerBasket;
    private GameObject _spawnedModel;
    private GameObject _modelTemplate;

    private void OnEnable()
    {
        _mainContainer = GetComponentInParent<Transform>();
    }

    private void Start()
    {
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
        if (_spawnedModel != null)
            Destroy(_spawnedModel);

        if (Game.SkinHandler.ChoosenCargoSkin == null)
            _modelTemplate = _defaultSkin;
        else
            _modelTemplate = Game.SkinHandler.ChoosenCargoSkin.Object;

        _spawnedModel = Instantiate(_modelTemplate, transform.position, transform.rotation, transform);
    }
}
