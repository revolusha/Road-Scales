using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShopCell : MonoBehaviour
{
    [SerializeField] private GameObject _priceLabel;
    [SerializeField] private Color _ownedColor;
    [SerializeField] private Color _selectedItemColor;
    [SerializeField] private Color _notPurchasedAffordableColor;
    [SerializeField] private Color _notPurchasedColor;
    [SerializeField] private Image _background;
    [SerializeField] private Image _preview;

    private ShopItem _item;
    private TextMeshProUGUI _textField;

    private static Action OnCalledUpdating; 

    public ShopItem Item => _item;

    private void OnEnable()
    {
        _textField = GetComponentInChildren<TextMeshProUGUI>();
        OnCalledUpdating += UpdateCell;
    }

    private void OnDisable()
    {
        OnCalledUpdating -= UpdateCell;
    }

    public static void UpdateAll()
    {
        OnCalledUpdating?.Invoke();
    }

    public void AssignShopItem(ShopItem shopItem)
    {
        _item = shopItem;

        if (_item.Image != null)
            _preview.sprite = _item.Image;

        UpdateCell();
    }

    public void UpdateCell()
    {
        UpdateColor();

        if (_item.IsOwned == false)
        {
            _textField.text = _item.Price.ToString();
            ShowPriceLabel();
        }
        else
        {
            HidePriceLabel();
        }
    }

    public void HandleCellClick()
    {
        if (_item.IsSelected)
            return;

        if (_item.IsOwned)
        {
            _item.Select();
            Game.Instance.GetComponent<Saving>().StartSaving();
            UpdateAll();
            return;
        }

        if (_item.Price <= Game.Money.Balance)
            CallPurchaseConfirmationPanel();
        else
            Game.SoundPlayer.PlayErrorSound();
    }

    private void CallPurchaseConfirmationPanel()
    {
        Shop.OnRequestBuyPanel?.Invoke(this);
    }

    private void ShowPriceLabel()
    {
        if (_priceLabel != null)
            _priceLabel.SetActive(true);
    }

    private void HidePriceLabel()
    {
        if (_priceLabel != null)
            _priceLabel.SetActive(false);
    }

    private void UpdateColor()
    {
        if (_item.IsSelected)
        {
            _background.color = _selectedItemColor;
            return;
        }

        if (_item.IsOwned)
        {
            _background.color = _ownedColor;
            return;
        }

        if (_item.Price <= Game.Money.Balance)
            _background.color = _notPurchasedAffordableColor;
        else
            _background.color = _notPurchasedColor;
    }
}