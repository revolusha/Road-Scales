using System;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private int _weight = 0;
    private GameObject _basketModel;

    public Action OnWeightChanged;
    public Action OnTouchedByObstacle;

    public int CargoCount => _weight;

    private void OnEnable()
    {
        SpawnBasketModel();
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

    private void SpawnBasketModel()
    {
        if (_basketModel != null)
            Destroy(_basketModel);

        _basketModel = Instantiate(Game.SkinHandler.ChoosenBasketSkin.Object, transform);
    }
}
