using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public static class Loading
{
    private static bool _isLoadingDone = false;
    
    private static PlayerInfo _playerInfo;

    public static Action OnFullLoadingFinished;

    public static bool IsLoadingDone => _isLoadingDone;

    public static void Load()
    {
        PlayerAccount.GetPlayerData(HandleLoadedData, HandleFailedLoading);
    }

    public static void LoadLocalPlayerData()
    {
        if (PlayerPrefs.HasKey(Saving.PrefsKey))
            LoadData(JsonUtility.FromJson<PlayerInfo>(
                Encryptor.Decrypt(PlayerPrefs.GetString(Saving.PrefsKey))));
        else
            LoadDefaultData();
    }

    private static void FinishLoading()
    {
        _isLoadingDone = true;
        StartGameInitializer.TryFinishInitialization();
    }

    private static void HandleLoadedData(string data)
    {
        const string EmptyReturnedString = "{}";

        if (data == EmptyReturnedString)
            LoadLocalPlayerData();
        else
            LoadNewestPlayerInfo(data);
    }

    private static void HandleFailedLoading(string _)
    {
        LoadLocalPlayerData();
    }

    private static void LoadNewestPlayerInfo(string cloudData)
    {
        PlayerInfo cloudPlayerInfo = JsonUtility.FromJson<PlayerInfo>(cloudData);

        if (PlayerPrefs.HasKey(Saving.PrefsKey) == false)
        {
            LoadData(cloudPlayerInfo);
            return;
        }

       PlayerInfo localPlayerInfo = JsonUtility.FromJson<PlayerInfo>(
            PlayerPrefs.GetString(Encryptor.Decrypt(Saving.PrefsKey)));

        if (localPlayerInfo.SavedTime < cloudPlayerInfo.SavedTime)
            LoadData(cloudPlayerInfo);
        else
            LoadData(localPlayerInfo);
    }

    private static void LoadData(PlayerInfo data)
    {
        _playerInfo = data;
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

    private static void LoadDefaultData()
    {
        Game.SkinHandler.PlayerSkins[0].Select();
        Game.SkinHandler.CargoSkins[0].Select();
        Game.SkinHandler.BasketSkins[0].Select();
        FinishLoading();
    }

    private static void LoadSkinsInfo(ShopItem[] shopItems, bool[] skinsFlags, int selectedIndex)
    {
        for (int i = 0; i < skinsFlags.Length; i++)
            shopItems[i].SetOwning(skinsFlags[i]);

        shopItems[selectedIndex].Select();
    }
}