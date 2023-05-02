using System;

public class PlayerSkinItem : ShopItem
{
    public PlayerSkinItem(ShopSerializableItem item) : base(item)
    {
        OnSkinSelected += Deselect;
    }

    ~PlayerSkinItem()
    {
        OnSkinSelected -= Deselect;
    }

    private static Action OnSkinSelected;

    public override void Select()
    {
        OnSkinSelected?.Invoke();
        Game.SkinHandler.SelectItem(this);
        base.Select();
    }
}