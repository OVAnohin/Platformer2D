using UnityEngine;

public class CloudsMove : MonoBehaviour
{
  private float _speed = 0;
  private float _elapsedTime = 0;
  private bool _moveRight = true;
  private float _moveTime = 0;

  private void Start()
  {
    _moveTime = Random.Range(10, 20);
    _speed = Random.Range(1, 5);
  }

  private void Update()
  {
    transform.Translate(Vector2.left * _speed * Time.deltaTime);

    if (_elapsedTime <= 0)
    {
      _elapsedTime = _moveTime;
      Flip();
    }

    _elapsedTime -= Time.deltaTime;
  }

  private void Flip()
  {
    if (_moveRight)
      transform.eulerAngles = new Vector3(0, -180, 0);
    else
      transform.eulerAngles = new Vector3(0, 0, 0);

    _moveRight = !_moveRight;
  }
}
