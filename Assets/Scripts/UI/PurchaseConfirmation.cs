using TMPro;
using UnityEngine;

public class PurchaseConfirmation : DialogWindowBase
{
    [SerializeField] protected TextMeshProUGUI _costText;

    private ShopCell _desiredItemCell;

    public void BuyItem()
    {
        if (Game.Money.TryToWithdrawMoney(_desiredItemCell.Item.Price))
        {
            _desiredItemCell.Item.SetOwning(true);
            ShopCell.UpdateAll();
            HidePanel();
        }
    }

    public void ShowPanel(ShopCell cell)
    {
        ShowPanel();
        _desiredItemCell = cell;
        _costText.text = cell.Item.Price.ToString();
    }
}