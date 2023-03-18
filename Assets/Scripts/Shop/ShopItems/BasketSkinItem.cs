using System;

public class BasketSkinItem : ShopItem
{
    public BasketSkinItem(ShopSerializableItem item) : base(item)
    {
        OnAnotherItemSelect += Deselect;
    }

    ~BasketSkinItem()
    {
        OnAnotherItemSelect -= Deselect;
    }

    private static Action OnAnotherItemSelect;

    public override void Select()
    {
        OnAnotherItemSelect?.Invoke();
        base.Select();
    }
}