using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class StartPanelFromKeyHider : MonoBehaviour
{
    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        //_button.in += Trigger;
    }

    private Button.ButtonClickedEvent Trigger()
    {
        throw new NotImplementedException();
    }
}