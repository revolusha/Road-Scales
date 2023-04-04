using System;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private GameObject _defaultBasketTemplate;

    private int _weight = 0;
    private GameObject _basketModel;

    public Action OnWeightChanged;
    public Action OnTouchedByObstacle;

    public int CargoCount => _weight;

    private void OnEnable()
    {
        if (Game.SkinHandler.ChoosenBasketSkin == null)
            SpawnBasketModel(_defaultBasketTemplate);
        else
            SpawnBasketModel(Game.SkinHandler.ChoosenBasketSkin.Object);

        SkinHandler.OnBasketSkinChanged += SpawnBasketModel;
    }

    private void OnDisable()
    {
        SkinHandler.OnBasketSkinChanged -= SpawnBasketModel;
    }

    public void UpdateWeight()
    {
        _weight = GetComponentsInChildren<Cargo>().Length;
        OnWeightChanged?.Invoke();
    }

    private void SpawnBasketModel(GameObject skinTemplate)
    {
        if (_basketModel != null)
            Destroy(_basketModel);

        _basketModel = Instantiate(skinTemplate, transform);
    }
}
