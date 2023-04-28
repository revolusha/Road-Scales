using System;
using UnityEngine;

public class Money
{
    public const int CargoWorth = 5;
    public const int LevelReward = 300;

    private int _balance;
    private int _lastCatchedCargo;
    private int _scores;

    public int Balance => _balance;
    public int LastCatchedCargo => _lastCatchedCargo;
    public int Score => _scores;

    public Action<int> OnRewardGained;
    public Action<int> OnMoneyBalanceChanged;

    public Money()
    {
        Debug.Log("Money");
        _balance = 0;
    }

    public void LoadMoney(int amount, int score)
    {
        Debug.Log("LoadMoney");
        _balance = amount;
        _scores = score;
    }

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

        _lastCatchedCargo = cargoCount;
        DepositMoney(reward);
        _scores += cargoCount;
        OnMoneyBalanceChanged?.Invoke(_balance);
        OnRewardGained?.Invoke(reward);
    }

    public static string ConvertBalanceIntegerToString(int value)
    {
        Debug.Log("ConvertBalanceIntegerToString");
        const string Separator = ",";
        const int Devider = 1000;
        const int TwoDigit = 99;
        const int OneDigit = 9;

        if (value <= 0)
            return "0";

        int integer = value;
        int remainder;
        string result = "";

        while (integer > 0)
        {
            remainder = integer % Devider;
            integer /= Devider;

            if (result.Length > 0)
                result = Separator + result;

            if (remainder > TwoDigit || integer <= 0)
                result = remainder.ToString() + result;
            else if (remainder > OneDigit)
                result = "0" + remainder.ToString() + result;
            else if (remainder < OneDigit)
                result = "00" + remainder.ToString() + result;
        }

        return result;
    }
}