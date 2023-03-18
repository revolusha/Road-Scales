using System;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private GameObject _defaultBasketTemplate;

    private int _weight = 0;

    public Action OnWeightChanged;
    public Action OnTouchedByObstacle;

    public float Weight => _weight;
    public int CargoCount
    {
        get 
        {
            Cargo[] catchedCargo = GetComponentsInChildren<Cargo>();
            
            return catchedCargo.Length;
        }
    }

    private void OnEnable()
    {
        if (Game.SkinHandler.ChoosenBasketSkin == null)
            Instantiate(_defaultBasketTemplate, transform);
        else
            Instantiate(Game.SkinHandler.ChoosenBasketSkin.Object, transform);
    }

    public void UpdateWeight()
    {
        _weight = GetComponentsInChildren<Cargo>().Length;
        OnWeightChanged?.Invoke();
    }
}
