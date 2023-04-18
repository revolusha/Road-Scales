public class RewardPanel : DialogWindowBase
{
    private const int Reward = 300;

    private bool _isRewardGained;

    private new void OnEnable()
    {
        base.OnEnable();
        Advertisement.OnRewardAdClosedFailed += OnRewardedClosedEvent;
        Advertisement.OnRewardAdClosedSuccessful += OnRewardedSuccessEvent;
    }

    private void OnDisable()
    {
        Advertisement.OnRewardAdClosedFailed -= OnRewardedClosedEvent;
        Advertisement.OnRewardAdClosedSuccessful -= OnRewardedSuccessEvent;
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
    }

    private void OnRewardedClosedEvent()
    {
        if (_isRewardGained)
            Game.Money.DepositMoney(Reward);

        HidePanel();
    }
}