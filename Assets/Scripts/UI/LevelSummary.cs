using TMPro;
using UnityEngine;

public class LevelSummary : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;

    private void OnEnable()
    {
        Game.Money.OnRewardGained += ShowResults;
    }

    private void OnDisable()
    {
        Game.Money.OnRewardGained -= ShowResults;
    }

    private void ShowResults(int earnedMoney)
    {
        _textField.text = "+ " + Money.ConvertBalanceIntegerToString(earnedMoney);
    }
}