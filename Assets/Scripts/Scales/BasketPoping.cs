using UnityEngine;

[RequireComponent(typeof(Animator))]

public class BasketPoping : MonoBehaviour
{
    private const string Trigger = "Pop";

    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void Pop()
    {
        _animator.SetTrigger(Trigger);
    }
}