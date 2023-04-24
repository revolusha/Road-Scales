using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class KeyButtonInvoker : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keyCodesToInvokeButton;

    private void Update()
    {
        for (int i = 0; i < _keyCodesToInvokeButton.Length; i++)
            if (Input.GetKey(_keyCodesToInvokeButton[i]))
                GetComponent<Button>().onClick.Invoke();
    }
}