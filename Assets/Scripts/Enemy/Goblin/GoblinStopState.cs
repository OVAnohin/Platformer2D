using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GoblinStopState : State
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("Idle");
    }
}
