using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public static class Loading
{
    private static PlayerInfo _playerInfo;

    public static Action OnLoadingFinished;

    private static readonly Action<string> OnDataStringGot = HandleLoadedData;
    private static readonly Action<string> OnDataStringFailed = HandleFailedLoading;

    public static IEnumerator Load()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnLoadingFinished?.Invoke();
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        PlayerAccount.GetPlayerData(OnDataStringGot, OnDataStringFailed);
    }

    private static void HandleFailedLoading()
    {
        OnLoadingFinished?.Invoke();
    }

    private static void HandleFailedLoading(string _)
    {
        HandleFailedLoading();
    }

    private static void HandleLoadedData(string data)
    {
        const string EmptyReturnedString = "{}";

        if (data == EmptyReturnedString)
            HandleFailedLoading("");

        _playerInfo = JsonUtility.FromJson<PlayerInfo>(data);
        LoadGeneralData();
        LoadSkinsData();
        OnLoadingFinished?.Invoke();
    }

    private static void LoadGeneralData()
    {
        Game.LevelHandler.SwitchToLevel(_playerInfo.Level);
        Game.Money.LoadMoney(_playerInfo.Coins);
        Game.SoundPlayer.SetVolume(_playerInfo.SoundVolume);
        Game.MusicPlayer.SetVolume(_playerInfo.MusicVolume);
    }

    private static void LoadSkinsData()
    {
        LoadSkinsInfo(Game.SkinHandler.PlayerSkins, _playerInfo.PlayerSkins, _playerInfo.ChoosenPlayerSkin);
        LoadSkinsInfo(Game.SkinHandler.CargoSkins, _playerInfo.CargoSkins, _playerInfo.ChoosenCargoSkin);
        LoadSkinsInfo(Game.SkinHandler.BasketSkins, _playerInfo.BasketSkins, _playerInfo.ChoosenBasketSkin);
    }

    private static void LoadSkinsInfo(ShopItem[] shopItems, bool[] skinsFlags, int selectedIndex)
    {
        for (int i = 0; i < skinsFlags.Length; i++)
            shopItems[i].SetOwning(skinsFlags[i]);

        shopItems[selectedIndex].Select();
    }
}
