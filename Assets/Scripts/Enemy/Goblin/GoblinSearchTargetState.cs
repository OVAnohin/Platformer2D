using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Goblin))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class GoblinSearchTargetState : State
{
    [SerializeField] private float _timeSearch;
    [SerializeField] private float _distanceCheck;
    [SerializeField] private LayerMask _whatIsGround;

    public bool IsTargetFound { get; private set; }
    public bool IsEndSearch { get; private set; }

    private float _elapsedTime = 0;
    private bool _lookToRight;
    private bool _isSwhithed;
    private int _layerMask;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private ContactFilter2D _contactFilter2D;
    private Vector2 _directionMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        IsTargetFound = false;
        IsEndSearch = false;
        _animator.Play("Idle");
        _layerMask = LayerMask.GetMask(LayerMask.LayerToName(Target.gameObject.layer));
        _isSwhithed = false;
        _elapsedTime = _timeSearch;
        _contactFilter2D.layerMask = _whatIsGround;
        _contactFilter2D.useLayerMask = true;

        if (transform.eulerAngles.y == 180f)
            _lookToRight = true;
        else
            _lookToRight = false;

        StartCoroutine(SearchTarget());
    }

    private IEnumerator SearchTarget()
    {
        while (_elapsedTime > 0)
        {
            CalculationOfDirection();
            if (_lookToRight)
                IsTargetFound = RayCastToTarget(Vector2.right);
            else
                IsTargetFound = RayCastToTarget(Vector2.left);

            if (IsTargetFound)
                break;

            if (_elapsedTime < _timeSearch / 2 && _isSwhithed == false)
                SwitchDirection();

            _elapsedTime -= Time.deltaTime;

            yield return null;
        }

        IsEndSearch = true;
    }

    private bool RayCastToTarget(Vector2 direction)
    {
        RaycastHit2D[] hit2Ds = new RaycastHit2D[1];

        bool isTargetFound = Physics2D.Raycast(transform.position, direction, _distanceCheck, _layerMask);
        int isPathClear = _rigidbody2D.Cast(_directionMove, _contactFilter2D, hit2Ds, Vector2.Distance(transform.position, Target.transform.position));

        return isTargetFound && isPathClear == 0;
    }

    private void CalculationOfDirection()
    {
        Vector2 batPosition = transform.position;
        Vector2 targetPosition = Target.transform.position;
        _directionMove = Vector3.Normalize(targetPosition - batPosition);
    }

    private void SwitchDirection()
    {
        if (_lookToRight)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, -180, 0);

        _lookToRight = !_lookToRight;
        _isSwhithed = true;
    }
}
