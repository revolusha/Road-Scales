using System;
using UnityEngine;

public class Basket : MonoBehaviour
{
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

    public void UpdateWeight()
    {
        _weight = GetComponentsInChildren<Cargo>().Length;
        OnWeightChanged?.Invoke();
    }
}
