using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public static class Loading
{
    private static bool _isLoadingDone = false;

    private static PlayerInfo _playerInfo;

    public static Action OnFullLoadingFinished;

    public static IEnumerator Load()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        FinishLoading();
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        PlayerAccount.GetPlayerData(HandleLoadedData, HandleFailedLoading);
    }

    public static void FinishLoading()
    {
        _isLoadingDone = true;
        TryCompleteLoading();
    }

    public static void TryCompleteLoading()
    {
        Debug.Log("loaded " + _isLoadingDone + " || localized " + SdkAndJavascriptHandler.IsLocalized);
        if (_isLoadingDone && SdkAndJavascriptHandler.IsLocalized)
            OnFullLoadingFinished?.Invoke();
    }

    private static void HandleFailedLoading(string _)
    {
        FinishLoading();
    }

    private static void HandleLoadedData(string data)
    {
        const string EmptyReturnedString = "{}";

        if (data == EmptyReturnedString)
            HandleFailedLoading("");

        _playerInfo = JsonUtility.FromJson<PlayerInfo>(data);
        Game.Instance.SetLastLevelFlag(_playerInfo.IsGotBadge);
        LoadGeneralData();
        LoadSkinsData();
        FinishLoading();
    }

    private static void LoadGeneralData()
    {
        Game.LevelHandler.SwitchToLevel(_playerInfo.Level);
        Game.Money.LoadMoney(_playerInfo.Coins, _playerInfo.Score);
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