using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerSkinComponentHandler : MonoBehaviour
{
    [SerializeField] private Behaviour[] _componentsToActivateOnStart;

    private const string TriggerStart = "Start";
    private const string TriggerDance = "Dance";

    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();

        for (int i = 0; i < _componentsToActivateOnStart.Length; i++)
        {
            _componentsToActivateOnStart[i].enabled = false;
        }
    }

    public void StartMoving()
    {
        for (int i = 0; i < _componentsToActivateOnStart.Length; i++)
        {
            _componentsToActivateOnStart[i].enabled = true;
        }

        _animator.SetTrigger(TriggerStart);
    }

    public void Finish()
    {
        _animator.SetTrigger(TriggerDance);
    }
}
