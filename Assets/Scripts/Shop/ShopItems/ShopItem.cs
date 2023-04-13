using UnityEngine;

public abstract class ShopItem
{
    private readonly GameObject _object;
    private readonly Sprite _image;
    private readonly string _name;
    private readonly int _price;

    private bool _isOwned = false;
    private bool _isSelected = false;

    public ShopItem(ShopSerializableItem item)
    {
        _object = item.ObjectForSell;
        _image = item.Image;
        _name = item.Name;
        _price = item.Price;
        _isOwned = false;
        _isSelected = false;
    }

    public GameObject Object => _object;
    public Sprite Image => _image;
    public string Name => _name;
    public int Price => _price;
    public bool IsOwned => _isOwned;
    public bool IsSelected => _isSelected;

    public void Deselect()
    {
        _isSelected = false;
    }

    public virtual void Select()
    {
        _isSelected = true;
    }

    public void SetOwning(bool isOwnedByPlayer)
    {
        _isOwned = isOwnedByPlayer;
    }
}