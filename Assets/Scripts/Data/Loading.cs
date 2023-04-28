using Agava.YandexGames;
using System;
using UnityEngine;

public static class Loading
{
    private static bool _isLoadingDone = false;
    
    private static PlayerInfo _playerInfo;

    public static Action OnFullLoadingFinished;

    public static bool IsLoadingDone => _isLoadingDone;

    public static void Load()
    {
        Debug.Log("Load");
        PlayerAccount.GetPlayerData(HandleLoadedData, HandleFailedLoading);
    }

    public static void FinishLoading()
    {
        Debug.Log("FinishLoading");
        _isLoadingDone = true;
        StartGameInitializer.TryFinishInitialization();
    }

    public static void TryCompleteLoading()
    {
        Debug.Log("TryCompleteLoading " + _isLoadingDone + SdkAndJavascriptHandler.IsLocalized);
        if (_isLoadingDone && SdkAndJavascriptHandler.IsLocalized)
            OnFullLoadingFinished?.Invoke();
    }

    private static void HandleFailedLoading(string _)
    {
        FinishLoading();
    }

    private static void HandleLoadedData(string data)
    {

        Debug.Log("HandleLoadedData");
        const string EmptyReturnedString = "{}";

        if (data == EmptyReturnedString)
            HandleFailedLoading("");

        _playerInfo = JsonUtility.FromJson<PlayerInfo>(data);
        Game.Instance.SetFlags(_playerInfo.IsGotBadge, _playerInfo.IsTutorialFinished);
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