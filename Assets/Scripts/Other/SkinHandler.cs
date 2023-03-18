using System;
using UnityEngine;

public class SkinHandler : MonoBehaviour
{
    private PlayerSkinItem _choosenPlayerSkin;
    private CargoSkinItem _choosenCargoSkin;
    private BasketSkinItem _choosenBasketSkin;

    private PlayerSkinItem[] _playerSkins;
    private CargoSkinItem[] _cargoSkins;
    private BasketSkinItem[] _basketSkins;

    public static Action<GameObject> OnPlayerSkinChanged;
    public static Action<GameObject> OnCargoSkinChanged;
    public static Action<GameObject> OnBasketSkinChanged;

    public PlayerSkinItem ChoosenPlayerSkin => _choosenPlayerSkin;
    public CargoSkinItem ChoosenCargoSkin => _choosenCargoSkin;
    public BasketSkinItem ChoosenBasketSkin => _choosenBasketSkin;
    public PlayerSkinItem[] PlayerSkins => _playerSkins;
    public CargoSkinItem[] CargoSkins => _cargoSkins;
    public BasketSkinItem[] BasketSkins => _basketSkins;

    public void LoadShopItems(PlayerSkinItem[] playerSkins, CargoSkinItem[] cargoSkins, BasketSkinItem[] basketSkins)
    {
        _playerSkins = playerSkins;
        _cargoSkins = cargoSkins;
        _basketSkins = basketSkins;
        _playerSkins[0].SetOwning(true);
        _cargoSkins[0].SetOwning(true);
        _basketSkins[0].SetOwning(true);
        _playerSkins[0].Select();
        _cargoSkins[0].Select();
        _basketSkins[0].Select();
    }

    public void SelectItem(PlayerSkinItem item)
    {
        _choosenPlayerSkin = item;
        OnPlayerSkinChanged?.Invoke(item.Object);
    }

    public void SelectItem(CargoSkinItem item)
    {
        _choosenCargoSkin = item;
        OnCargoSkinChanged?.Invoke(item.Object);
    }

    public void SelectItem(BasketSkinItem item)
    {
        _choosenBasketSkin = item;
        OnBasketSkinChanged?.Invoke(item.Object);
    }
}
