using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoblinSearchTargetState))]
[RequireComponent(typeof(Goblin))]
[RequireComponent(typeof(Animator))]
public class GoblinMoverState : State
{
    private System.Random rand = new System.Random();

    private float _speed;
    private Animator _animator;

    private void Awake()
    {
        _speed = GetComponent<Goblin>().GetSpeed;
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("Run");
        if (!GetComponent<GoblinSearchTargetState>().IsTargetFound)
        {
            if (rand.Next(0, 2) == 1)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else
                transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}
