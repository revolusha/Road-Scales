using System;

public class CargoSkinItem : ShopItem
{
    public CargoSkinItem(ShopSerializableItem item) : base(item)
    {
        OnAnotherItemSelect += Deselect;
    }

    ~CargoSkinItem()
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