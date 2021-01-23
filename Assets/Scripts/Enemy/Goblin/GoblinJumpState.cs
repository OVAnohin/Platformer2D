using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Goblin))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class GoblinJumpState : State
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRadius;

    public bool IsEndJump { get; private set; }

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private float _timeLeft = 0.1f;
    private int _direction;
    private float _speed;

    private void Awake()
    {
        _speed = GetComponent<Goblin>().GetSpeed * 4;
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _direction = transform.rotation.y == 0 ? -1 : 1;
        _animator.Play("Jump");
        IsEndJump = false;
    }

    private void Update()
    {
        if (IsEndJump == true)
            return;

        Jump();
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_direction * _speed, _rigidbody2D.velocity.y);
        _timeLeft -= Time.deltaTime;

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);

        if (_isGrounded && IsEndJump == false && _timeLeft <= 0)
            IsEndJump = true;

        if (IsEndJump == false && _isGrounded)
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
    }
}
