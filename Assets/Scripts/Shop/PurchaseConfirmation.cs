using TMPro;
using UnityEngine;

public class PurchaseConfirmation : MonoBehaviour
{
    [SerializeField] private GameObject[] _elementsToHide;
    [SerializeField] private TextMeshProUGUI _costText;

    private ShopCell _desiredItemCell;

    private void OnEnable()
    {
        HidePanel();
    }

    public void BuyItem()
    {
        if (Game.Money.TryToWithdrawMoney(_desiredItemCell.Item.Price))
        {
            _desiredItemCell.Item.SetOwning(true);
            ShopCell.UpdateAll();
            HidePanel();
        }
    }

    public void HidePanel()
    {
        foreach (GameObject element in _elementsToHide)
            element.SetActive(false);
    }

    public void ShowPanel(ShopCell cell)
    {
        foreach (GameObject element in _elementsToHide)
            element.SetActive(true);

        _desiredItemCell = cell;
        _costText.text = cell.Item.Price.ToString();
    }
}