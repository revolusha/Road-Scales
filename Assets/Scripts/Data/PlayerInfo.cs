using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int Level;
    public int Coins;
    public int ChoosenPlayerSkin;
    public int ChoosenCargoSkin;
    public int ChoosenBasketSkin;

    public float SoundVolume;
    public float MusicVolume;

    public bool[] PlayerSkins;
    public bool[] CargoSkins;
    public bool[] BasketSkins;

    public PlayerInfo()
    {
        Level = Game.LevelHandler.CurrentLevelIndex;
        Coins = Game.Money.Balance;
        ChoosenPlayerSkin = Game.SkinHandler.ChoosenPlayerSkinIndex;
        ChoosenCargoSkin = Game.SkinHandler.ChoosenCargoSkinIndex;
        ChoosenBasketSkin = Game.SkinHandler.ChoosenBasketSkinIndex;
        SoundVolume = Game.SoundPlayer.Volume;
        MusicVolume = Game.MusicPlayer.Volume;
        PlayerSkins = GetOwningFlags(Game.SkinHandler.PlayerSkins);
        CargoSkins = GetOwningFlags(Game.SkinHandler.CargoSkins);
        BasketSkins = GetOwningFlags(Game.SkinHandler.BasketSkins);
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    private bool[] GetOwningFlags(ShopItem[] shopItems)
    {
        bool[] flags = new bool[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
            flags[i] = shopItems[i].IsOwned;

        return flags;
    }
}