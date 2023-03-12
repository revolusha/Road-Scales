using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    private GameObject _objectForSell;
    private Sprite _image;
    private string _name;
    private int _price;

    private bool _isOwned = false;
    private bool _isSelected = false;

    public ShopItem(ShopSerializableItem item)
    {
        _objectForSell = item.ObjectForSell;
        _image = item.Image;
        _name = item.Name;
        _price = item.Price;
        _isOwned = false;
        _isSelected = false;
    }

    public GameObject ObjectForSell => _objectForSell;
    public Sprite Image => _image;
    public string Name => _name;
    public int Price => _price;
    public bool IsOwned => _isOwned;
    public bool IsSelected => _isSelected;

    public void Deselect()
    {
        _isSelected = false;
    }

    public void Select()
    {
        _isSelected = true;
    }

    public void SetOwning(bool isOwnedByPlayer)
    {
        _isOwned = isOwnedByPlayer;
    }
}
