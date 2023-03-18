using UnityEngine;

[RequireComponent(typeof(SkinHandler))]

public class SkinLoader : MonoBehaviour
{
    [SerializeField] private PlayerSkinItemTemplate[] _playerSkins;
    [SerializeField] private CargoSkinItemTemplate[] _cargoSkins;
    [SerializeField] private BasketSkinItemTemplate[] _basketSkins;

    private SkinHandler handler;

    private void OnEnable()
    {
        handler = GetComponent<SkinHandler>();
        handler.LoadShopItems(
            CreateShopItemList(_playerSkins),
            CreateShopItemList(_cargoSkins),
            CreateShopItemList(_basketSkins));
    }

    private PlayerSkinItem[] CreateShopItemList(PlayerSkinItemTemplate[] playerSkinTemplates)
    {
        PlayerSkinItem[] shopItems = new PlayerSkinItem[playerSkinTemplates.Length];

        for (int i = 0; i < shopItems.Length; i++)
            shopItems[i] = new PlayerSkinItem(playerSkinTemplates[i]);

        return shopItems;
    }

    private CargoSkinItem[] CreateShopItemList(CargoSkinItemTemplate[] playerSkinTemplates)
    {
        CargoSkinItem[] shopItems = new CargoSkinItem[playerSkinTemplates.Length];

        for (int i = 0; i < shopItems.Length; i++)
            shopItems[i] = new CargoSkinItem(playerSkinTemplates[i]);

        return shopItems;
    }

    private BasketSkinItem[] CreateShopItemList(BasketSkinItemTemplate[] playerSkinTemplates)
    {
        BasketSkinItem[] shopItems = new BasketSkinItem[playerSkinTemplates.Length];

        for (int i = 0; i < shopItems.Length; i++)
            shopItems[i] = new BasketSkinItem(playerSkinTemplates[i]);

        return shopItems;
    }
}
