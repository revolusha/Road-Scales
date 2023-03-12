using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinItem", menuName = "Road Scale Game/Shop Item/Player Skin Item")]

public class PlayerSkinItem : ShopItem
{
    public PlayerSkinItem(ShopSerializableItem item) : base(item) { }
}
