using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkinHandler))]

public class SkinLoader : MonoBehaviour
{
    [SerializeField] private PlayerSkinItemTemplate[] _playerSkins;
    [SerializeField] private CargoSkinItemTemplate[] _cargoSkins;
    [SerializeField] private BasketSkinItemTemplate[] _basketSkins;

    private static bool _isInitialized = false;

    private SkinHandler _handler;

    private void Start()
    {
        _handler = GetComponent<SkinHandler>();
        _handler.LoadShopItems(
            CreateShopItemList(_playerSkins),
            CreateShopItemList(_cargoSkins),
            CreateShopItemList(_basketSkins));
        _isInitialized = true;
    }

    private PlayerSkinItem[] CreateShopItemList(PlayerSkinItemTemplate[] playerSkinTemplates)
    {
        PlayerSkinItem[] shopItems = new PlayerSkinItem[playerSkinTemplates.Length];

        for (int i = 0; i < shopItems.Length; i++)
            shopItems[i] = new PlayerSkinItem(playerSkinTemplates[i]);

        return shopItems;
    }

    private CargoSkinItem[] CreateShopItemList(CargoSkinItemTemplate[] cargoSkinTemplates)
    {
        CargoSkinItem[] shopItems = new CargoSkinItem[cargoSkinTemplates.Length];

        for (int i = 0; i < shopItems.Length; i++)
            shopItems[i] = new CargoSkinItem(cargoSkinTemplates[i]);

        return shopItems;
    }

    private BasketSkinItem[] CreateShopItemList(BasketSkinItemTemplate[] basketSkinTemplates)
    {
        BasketSkinItem[] shopItems = new BasketSkinItem[basketSkinTemplates.Length];

        for (int i = 0; i < shopItems.Length; i++)
            shopItems[i] = new BasketSkinItem(basketSkinTemplates[i]);

        return shopItems;
    }

    public static IEnumerator Initialize()
    {
        while (_isInitialized == false)
            yield return null;
    }
}