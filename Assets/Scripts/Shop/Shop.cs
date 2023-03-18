using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private PurchaseConfirmation _confirmation;
    [Header("Shop Items")]
    [SerializeField] private GameObject _shopCellTemplate;
    [SerializeField] private GameObject _playerSkinsContainer;
    [SerializeField] private GameObject _cargoSkinsContainer;
    [SerializeField] private GameObject _basketSkinsContainer;

    private readonly List<ShopCell> _playerSkins = new();
    private readonly List<ShopCell> _cargoSkins = new();
    private readonly List<ShopCell> _basketSkins = new();

    public static Action<ShopCell> OnItemClicked;

    private void OnEnable()
    {
        OnItemClicked += ShowConfimationPanel;
    }

    private void Start()
    {
        CreateShopCells();
    }

    private void OnDisable()
    {
        OnItemClicked -= ShowConfimationPanel;
    }

    private void ShowConfimationPanel(ShopCell cell)
    {
        _confirmation.ShowPanel(cell);
    }    

    private void CreateShopCells()
    {
        CreateCategoryShopCells(Game.SkinHandler.PlayerSkins, _playerSkinsContainer.transform, _playerSkins);
        CreateCategoryShopCells(Game.SkinHandler.CargoSkins, _cargoSkinsContainer.transform, _cargoSkins);
        CreateCategoryShopCells(Game.SkinHandler.BasketSkins, _basketSkinsContainer.transform, _basketSkins);
        UpdateAllCells();
    }

    private void CreateCategoryShopCells(ShopItem[] shopItems, Transform parentTransform, List<ShopCell> shopCellList)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            ShopCell newCell = Instantiate(_shopCellTemplate, parentTransform).GetComponent<ShopCell>();

            newCell.AssignShopItem(shopItems[i]);
            shopCellList.Add(newCell);
        }
    }

    private void UpdateAllCells()
    {
        UpdateCategoryCells(_playerSkins);
        UpdateCategoryCells(_cargoSkins);
        UpdateCategoryCells(_basketSkins);
    }

    private void UpdateCategoryCells(List<ShopCell> categoryItems)
    {
        foreach (ShopCell cell in categoryItems)
            cell.UpdateCell();
    }
}
