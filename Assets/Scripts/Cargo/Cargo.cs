using UnityEngine;

public class Cargo : MonoBehaviour
{
    private Basket _containerBasket;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("+");
        if (other.TryGetComponent(out _containerBasket))
        {
            _containerBasket.IncreaseWeight();
            Debug.Log("+++");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("-");
        if (other.TryGetComponent(out _containerBasket))
        {
            _containerBasket.DecreaseWeight();
            Debug.Log("---");
        }
    }
}
