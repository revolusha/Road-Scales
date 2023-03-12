using UnityEngine;

public class SkinHandler : MonoBehaviour
{
    private PlayerSkinItem[] _playerSkins;
    private CargoSkinItem[] _cargoSkins;
    private BasketSkinItem[] _basketSkins;

    public PlayerSkinItem[] PlayerSkins => _playerSkins;
    public CargoSkinItem[] CargoSkins => _cargoSkins;
    public BasketSkinItem[] BasketSkins => _basketSkins;

    public void LoadShopItems()
    {

    }

    public void SelectItem(PlayerSkinItem item)
    {
        DeselectItemsCategory(_playerSkins);
        HighlightItem(item);
    }

    public void SelectItem(CargoSkinItem item)
    {
        DeselectItemsCategory(_cargoSkins);
        HighlightItem(item);
    }

    public void SelectItem(BasketSkinItem item)
    {
        DeselectItemsCategory(_basketSkins);
        HighlightItem(item);
    }

    private void HighlightItem(ShopItem item)
    {
        item.Select();
        ShopCell.UpdateAll();
    }

    private void DeselectItemsCategory(ShopItem[] itemsCategory)
    {
        for (int i = 0; i < itemsCategory.Length; i++)
            itemsCategory[i].Deselect();
    }
}
