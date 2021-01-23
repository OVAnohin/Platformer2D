using System.Collections;
using UnityEngine;

public class WarlocPatrol : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatsIsGround;
    [SerializeField] private float _distanceCheck;

    private bool _isWait = false;
    private bool _isMovigLeft = true;

    private void Update()
    {
        if (!_isWait)
            transform.Translate(Vector2.left * _speed * Time.deltaTime);

        bool isGap = Physics2D.Raycast(_groundCheck.position, Vector2.down, _distanceCheck, _whatsIsGround);

        if (isGap == false)
        {
            Flip();
            StartCoroutine(WaitTime());
        }
    }

    private IEnumerator WaitTime()
    {
        _isWait = true;
        float elapsedTime = _waitTime;

        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;
        }

        _isWait = false;
    }

    private void Flip()
    {
        if (_isMovigLeft)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);

        _isMovigLeft = !_isMovigLeft;
    }
}
