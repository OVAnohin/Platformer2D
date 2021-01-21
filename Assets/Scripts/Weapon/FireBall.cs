using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class FireBall : Weapon
{
  [SerializeField] private float _speed = 0;
  [SerializeField] private float _lifeTime = 0;

  private Rigidbody2D _rigidbody2D;
  private BoxCollider2D _collider2D;
  private bool _isMoveLeft;
  private float _elapsedTime;

  protected override void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.TryGetComponent<Player>(out Player player))
      player.TakeDamage(Damage);

    Destroy(gameObject);
  }

  public void Init(bool isMoveLeft)
  {
    _isMoveLeft = isMoveLeft;
  }

  public void EliminateSelf()
  {
    Destroy(gameObject);
  }

  private void OnEnable()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    _collider2D = GetComponent<BoxCollider2D>();
    _collider2D.isTrigger = true;
  }

  private void Start()
  {
    if (_isMoveLeft == false)
      transform.eulerAngles = new Vector3(0, -180, 0);

  }
  private void Update()
  {
    transform.Translate(Vector2.left * _speed * Time.deltaTime);
    if (_elapsedTime > _lifeTime)
      Destroy(gameObject);
    else
      _elapsedTime += Time.deltaTime;

  }
}
