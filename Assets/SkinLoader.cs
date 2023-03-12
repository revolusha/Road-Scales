using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinHandler))]

public class SkinLoader : MonoBehaviour
{
    private ShopSerializableItem[] _playerSkins;
    private ShopSerializableItem[] _cargoSkins;
    private ShopSerializableItem[] _basketSkins;

    private SkinHandler handler;

    private void OnEnable()
    {
        
    }

    private void CreateShopItem()
    {

    }
}
