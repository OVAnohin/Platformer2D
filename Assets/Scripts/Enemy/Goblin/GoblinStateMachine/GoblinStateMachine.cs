using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Goblin))]
public class GoblinStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    private Player _target;
    private float _speed;

    public State CurrentState => _currentState;

    private void Start()
    {
        _target = GetComponent<Goblin>().GetTarget;
        _speed = GetComponent<Goblin>().GetSpeed;

        Reset(_firstState);
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_target, _speed);
    }

    public void ResetOnDie()
    {
        _currentState.Exit();
        _currentState = _firstState;
        _currentState.Enter(_target, _speed);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target, _speed);
    }
}
