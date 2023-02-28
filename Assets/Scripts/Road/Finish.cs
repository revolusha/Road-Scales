using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]

public class Finish : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPlayerFinishedEvent;

    private Action OnPlayerFinished;

    public void TriggerFinishEvent()
    {
        OnPlayerFinishedEvent?.Invoke();
    }
}
