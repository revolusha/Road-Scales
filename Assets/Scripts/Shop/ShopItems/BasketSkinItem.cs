using System;

public class BasketSkinItem : ShopItem
{
    public BasketSkinItem(ShopSerializableItem item) : base(item)
    {
        OnSkinSelected += Deselect;
    }

    ~BasketSkinItem()
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