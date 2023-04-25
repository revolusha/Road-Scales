using UnityEngine;

[RequireComponent(typeof(Game))]

public class FocusHandler : MonoBehaviour
{
    private const float DefaultTimeScale = 1;

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Unpause();
        else
            Pause();
    }

    public static void Pause()
    {
        AudioMuter.MuteMusic(false);
        AudioMuter.MuteSound(false);
        Time.timeScale = 0;
    }

    public static void Unpause()
    {
        if (AudioMuter.IsMusicMuted == false)
            AudioMuter.UnmuteMusic();

        if (AudioMuter.IsSoundMuted == false)
            AudioMuter.UnmuteSound();

        Time.timeScale = DefaultTimeScale;
    }
}