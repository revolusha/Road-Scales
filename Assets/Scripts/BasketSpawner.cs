using UnityEngine;

[RequireComponent(typeof(Scales))]

public class BasketSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _basketTemplate;
    [Range(3.5f, 5f)]
    [SerializeField] private float _basketSpawnXOffset;

    private void OnEnable()
    {
        Vector3 leftPosition = transform.position + new Vector3(-_basketSpawnXOffset, 0, 0);
        Vector3 rightPosition = transform.position + new Vector3(_basketSpawnXOffset, 0, 0);

        Basket leftBasket = Instantiate(_basketTemplate, leftPosition, Quaternion.identity, transform)
            .GetComponent<Basket>();
        Basket rightBasket = Instantiate(_basketTemplate, rightPosition, Quaternion.identity, transform)
            .GetComponent<Basket>();

        GetComponent<Scales>().SetBaskets(leftBasket, rightBasket);
    }
}
