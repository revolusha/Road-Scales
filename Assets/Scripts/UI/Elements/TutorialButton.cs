using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class TutorialButton : MonoBehaviour
{
    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
    }

    public void Skip()
    {
        Tutorial.Skip();
    }

    public void HideButton()
    {
        StartCoroutine(HideButtonJob());
    }

    private IEnumerator HideButtonJob()
    {
        const float Delay = 2f;

        _button.interactable = false;

        yield return new WaitForSeconds(Delay);

        _button.interactable = true;
    }
}