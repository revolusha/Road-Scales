using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]

public class RewardButton : MonoBehaviour
{
    private Animator _animator;
    private Button _button;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
        TryShowButton();
    }

    public void CheckShowPermission()
    {
        StartCoroutine(WaitAndCheck());
    }

    private void SetAppearence(bool isShown)
    {
        _button.enabled = isShown;
        _animator.enabled = isShown;
    }

    private void TryShowButton()
    {
        if (Game.Advertisement.IsReadyToShowRewardAd)
            SetAppearence(true);
        else
            SetAppearence(false);
    }

    private IEnumerator WaitAndCheck()
    {
        const float Delay = .1f;

        yield return new WaitForSeconds(Delay);

        TryShowButton();
    }
}