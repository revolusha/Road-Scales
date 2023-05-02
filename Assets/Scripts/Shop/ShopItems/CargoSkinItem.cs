using System;

public class CargoSkinItem : ShopItem
{
    public CargoSkinItem(ShopSerializableItem item) : base(item)
    {
        OnSkinSelected += Deselect;
    }

    ~CargoSkinItem()
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