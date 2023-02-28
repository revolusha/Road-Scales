using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private Money _money;

    public static GameData Instance { get; private set; }
    public static Money Money => Instance._money;

    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;

        _money = new Money();
    }
}
