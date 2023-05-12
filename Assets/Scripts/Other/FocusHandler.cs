using UnityEngine;

[RequireComponent(typeof(Game))]

public class FocusHandler : MonoBehaviour
{
    private const float DefaultTimeScale = 1;

    private void OnEnable()
    {
        Advertisement.OnRewardAdOpened += Pause;
        Advertisement.OnRewardAdClosed += Unpause;
    }

    private void OnDisable()
    {
        Advertisement.OnRewardAdOpened -= Pause;
        Advertisement.OnRewardAdClosed -= Unpause;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Unpause();
        else
            Pause();
    }

    public static void Pause()
    {
        AudioMuter.Pause();
        Time.timeScale = 0;
    }

    public static void Unpause()
    {
        Time.timeScale = DefaultTimeScale;
        AudioMuter.Unpause();
    }
}