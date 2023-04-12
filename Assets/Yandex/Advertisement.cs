using UnityEngine;

public class Advertisement
{
    private const float MinimalIntervalToShowAd = 240;
    private static float _lastShowTime;

    public static bool CheckIfCanShowAd()
    {
        if (Time.time - _lastShowTime < MinimalIntervalToShowAd)
            return false;

        ResetTimer();
        return true;
    }

    public static void ResetTimer()
    {
        _lastShowTime = Time.time;
    }
}