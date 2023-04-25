using UnityEngine;

public class RewardPanel : DialogWindowBase
{
    [SerializeField] private RewardButton _button;

    private const int Reward = 300;

    private static bool _isRewardGained;

    public static bool IsRewardGained => _isRewardGained;

    private new void OnEnable()
    {
        base.OnEnable();
        Advertisement.OnRewardAdClosed += OnRewardedClosedEvent;
        Advertisement.OnRewardAdShowedSuccessful += OnRewardedSuccessEvent;
    }

    private void OnDisable()
    {
        Advertisement.OnRewardAdClosed -= OnRewardedClosedEvent;
        Advertisement.OnRewardAdShowedSuccessful -= OnRewardedSuccessEvent;
    }

    public void RequestReward()
    {
        _isRewardGained = false;
        Game.Advertisement.TryShowRewardAd();
    }

    public void SetCloseDelay()
    {
        const float Delay = 60;

        Game.Advertisement.DelayRewardAd(Delay);
    }

    private void OnRewardedSuccessEvent()
    {
        Game.Advertisement.ResetRewardTimer();
        _isRewardGained = true;
        _button.CheckShowPermission();
    }

    private void OnRewardedClosedEvent()
    {
        if (_isRewardGained)
            Game.Money.DepositMoney(Reward);

        HidePanel();
        _button.CheckShowPermission();
    }
}