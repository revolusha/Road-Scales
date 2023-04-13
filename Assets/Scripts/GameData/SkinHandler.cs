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

    public static Action OnPlayerSkinChanged;
    public static Action OnCargoSkinChanged;
    public static Action OnBasketSkinChanged;

    public PlayerSkinItem ChoosenPlayerSkin => _choosenPlayerSkin;
    public CargoSkinItem ChoosenCargoSkin => _choosenCargoSkin;
    public BasketSkinItem ChoosenBasketSkin => _choosenBasketSkin;
    public int ChoosenPlayerSkinIndex => GetIndexOfChoosenShopItem(_choosenPlayerSkin, _playerSkins);
    public int ChoosenCargoSkinIndex => GetIndexOfChoosenShopItem(_choosenCargoSkin, _cargoSkins);
    public int ChoosenBasketSkinIndex => GetIndexOfChoosenShopItem(_choosenBasketSkin, _basketSkins);
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
        OnPlayerSkinChanged?.Invoke();
    }

    public void SelectItem(CargoSkinItem item)
    {
        _choosenCargoSkin = item;
        OnCargoSkinChanged?.Invoke();
    }

    public void SelectItem(BasketSkinItem item)
    {
        _choosenBasketSkin = item;
        OnBasketSkinChanged?.Invoke();
    }

    private int GetIndexOfChoosenShopItem(ShopItem item, ShopItem[] shopItems)
    {
        for (int i = 0; i < shopItems.Length; i++)
            if (shopItems[i] == item)
                return i;

        return 0;
    }
}