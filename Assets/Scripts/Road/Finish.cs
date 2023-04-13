using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]

public class Finish : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPlayerFinishedEvent;

    public void TriggerFinishEvent()
    {
        OnPlayerFinishedEvent?.Invoke();
        Game.MusicPlayer.PlayWinSound();
    }
}