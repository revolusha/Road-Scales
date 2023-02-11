using UnityEngine;

public class CargoContainer : MonoBehaviour
{
    public static CargoContainer Instance;

    public static Transform Transform => Instance.transform;

    private void OnEnable()
    {
        Instance = this;
    }
}
