using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Goblin))]
[RequireComponent(typeof(Animator))]
public class GoblinJumpState : State
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    public bool IsEndJump { get; private set; }

    private float _a = -1;
    private float _b = 4;
    private float _x2;
    private float _x0;
    private float _positionY;
    private float _speed;
    private Animator _animator;

    private void Awake()
    {
        _speed = GetComponent<Goblin>().GetSpeed;
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("Jump");
        IsEndJump = false;
        _positionY = transform.position.y;

        СalculationJump();
        StartCoroutine(Jump());
    }

    private void СalculationJump()
    {
        float discriminant;
        discriminant = (_b * _b) - (4 * _a);
        _x2 = (-_b - Convert.ToSingle(Math.Sqrt(discriminant))) / (2 * _a);
        _x0 = -(_b / 2 * _a);
    }

    private IEnumerator Jump()
    {
        bool isGroundAbandoned = false;
        bool isExit = false;
        int direction = transform.rotation.y == 0 ? -1 : 1;

        while (!isExit)
        {

            float stepX = direction * _speed * Time.deltaTime;
            _x2 = stepX > 0 ? _x2 - stepX : _x2 + stepX;
            float y = _a * (_x2 * _x2) + _b * _x2;
            if (y > 0 || _x0 > _x2)
                transform.position = new Vector2(transform.position.x + stepX, _positionY + y);

            bool isOnGround = Physics2D.Raycast(_groundCheck.position, Vector2.down, 0.01f, _whatIsGround);

            if (!isOnGround)
                isGroundAbandoned = true;

            if (isGroundAbandoned && isOnGround)
                isExit = true;

            yield return null;
        }

        IsEndJump = true;
    }
}
