using System;
using UnityEngine;

[RequireComponent(typeof(Bat))]
[RequireComponent(typeof(Rigidbody2D))]
public class BatMover : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _wallCheckPoint;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _speed;

    private Player _target;
    private Vector2 _directionMove;
    private Rigidbody2D _rigidbody2D;
    private ContactFilter2D _contactFilter2D;

    private void Start()
    {
        _target = GetComponent<Bat>().GetTarget;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _contactFilter2D.layerMask = _whatIsGround;
        _contactFilter2D.useLayerMask = true;
    }

    private void Update()
    {
        CalculationOfDirection();

        RaycastHit2D[] hit2Ds = new RaycastHit2D[1];
        int isPathClear = _rigidbody2D.Cast(_directionMove, _contactFilter2D, hit2Ds, Vector2.Distance(transform.position, _target.transform.position));

        if (isPathClear == 0)
            transform.Translate(_directionMove * Time.deltaTime * _speed);
        else if (CheckIsWayToWall())
            transform.Translate(Vector2.up * Time.deltaTime * _speed);
        else
            transform.Translate((new Vector2(_directionMove.x, 0)) * Time.deltaTime * _speed);
    }

    private void CalculationOfDirection()
    {
        Vector2 batPosition = transform.position;
        Vector2 targetPosition = _target.transform.position;
        _directionMove = Vector3.Normalize(targetPosition - batPosition);
    }

    private bool CheckIsWayToWall()
    {
        if (_directionMove.x < 0)
        {
            if (CastRayToWall(Vector2.left))
                return true;
        }
        else
        {
            if (CastRayToWall(Vector2.right))
                return true;
        }

        return false;
    }

    private bool CastRayToWall(Vector2 directionCast)
    {
        return Physics2D.Raycast(_wallCheckPoint.position, directionCast, _wallCheckDistance, _whatIsGround);
    }
}
