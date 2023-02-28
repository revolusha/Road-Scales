using System;

public class Money
{
    public const int CargoWorth = 5;
    public const int LevelReward = 300;

    private int _balance;

    public int Balance => _balance;

    public Action<int> OnRewardGained;
    public Action<int> OnMoneyBalanceChanged;

    public void DepositMoney(int amount)
    {
        if (amount < 0)
            throw new Exception("Wrong amount to add to wallet balance!");

        _balance += amount;
        OnMoneyBalanceChanged?.Invoke(_balance);
    }

    public bool TryToWithdrawMoney(int amount)
    {
        if (_balance < amount)
            return false;

        _balance -= amount;
        OnMoneyBalanceChanged?.Invoke(_balance);
        return true;
    }

    public void GetReward(int cargoCount)
    {
        int reward = cargoCount * CargoWorth + LevelReward;

        DepositMoney(reward);
        OnMoneyBalanceChanged?.Invoke(_balance);
        OnRewardGained?.Invoke(reward);
    }

    public static string ConvertBalanceIntegerToString(int value)
    {
        const string Separator = ",";
        const int Devider = 1000;

        int integer = value;
        int remainder;
        string result = "";

        while (integer > 0)
        {
            remainder = integer % Devider;
            integer /= Devider;

            if (result.Length > 0)
                result = Separator + result;

            result = remainder.ToString() + result;
        }

        return result;
    }
}
