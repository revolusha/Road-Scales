using System;

public class PlayerSkinItem : ShopItem
{
    public PlayerSkinItem(ShopSerializableItem item) : base(item)
    {
        OnAnotherItemSelect += Deselect;
    }

    ~PlayerSkinItem()
    {
        OnAnotherItemSelect -= Deselect;
    }

    private static Action OnAnotherItemSelect;

    public override void Select()
    {
        OnAnotherItemSelect?.Invoke();
        Game.SkinHandler.SelectItem(this);
        base.Select();
    }
}