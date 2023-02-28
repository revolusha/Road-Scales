using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class MoneyCounter : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    private void OnEnable()
    {
        _textField = GetComponent<TextMeshProUGUI>();
        UpdateMoneyCounter(GameData.Money.Balance);
        GameData.Money.OnMoneyBalanceChanged += UpdateMoneyCounter;
    }

    private void OnDisable()
    {
        GameData.Money.OnMoneyBalanceChanged -= UpdateMoneyCounter;
    }

    private void UpdateMoneyCounter(int balance)
    {
        _textField.text = Money.ConvertBalanceIntegerToString(balance);
    }
}
