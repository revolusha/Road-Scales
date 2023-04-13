using UnityEngine;

public abstract class ShopSerializableItem : ScriptableObject
{
    [SerializeField] private GameObject _objectForSell;
    [SerializeField] private Sprite _image;
    [SerializeField] private string _name;
    [SerializeField] private int _price;

    public GameObject ObjectForSell => _objectForSell;
    public Sprite Image => _image;
    public string Name => _name;
    public int Price => _price;
}