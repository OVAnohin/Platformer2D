using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAtacks;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private AudioClip _attackSound;

    private Animator _animator;
    private bool _isUnderAttack;
    private int _damage;
    private float _attackRange;
    private Player _player;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _isUnderAttack = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _isUnderAttack == false)
            StartCoroutine(Attack());
    }

    public void TryToHitDamage()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
        foreach (var enemy in enemiesToDamage)
            enemy.GetComponent<Enemy>().TakeDamage(_damage);

        enemiesToDamage = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);
        var fireBall = enemiesToDamage.Where(element => element.GetComponent<FireBall>() != null).FirstOrDefault();
        if (fireBall != null)
            fireBall.GetComponent<FireBall>().EliminateSelf();
    }

    private void OnEnable()
    {
        _player.WeaponChanged += OnWeaponChanged;
    }
    private void OnDisable()
    {
        _player.WeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        _attackRange = weapon.GetAttackRange;
        _damage = weapon.GetDamage;
    }

    private IEnumerator Attack()
    {
        float elapsedTime = _timeBetweenAtacks;

        _isUnderAttack = true;
        _animator.SetTrigger("attack");
        _audioSource.clip = _attackSound;
        _audioSource.Play();

        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;
        }

        _isUnderAttack = false;
    }
}
