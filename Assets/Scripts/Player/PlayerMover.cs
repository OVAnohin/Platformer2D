using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _frontsCheck;
    [SerializeField] private LayerMask _whatIsGground;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private float _xWallForce;
    [SerializeField] private float _yWallForce;
    [SerializeField] private float _wallJumpTime;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private GameObject _dropEffect;

    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private bool _isGrounded;
    private bool _isTouchingFronts;
    private bool _wallSliding;
    private bool _wallJumping;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.velocity = new Vector2(input * _speed, _rigidbody2D.velocity.y);

        if (input > 0 && _facingRight == false)
            Flip();
        else if (input < 0 && _facingRight == true)
            Flip();

        if (input != 0)
            _animator.SetBool("isRunning", true);
        else
            _animator.SetBool("isRunning", false);

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGground);

        if ((Input.GetAxisRaw("Vertical") != 0) && _isGrounded == true)
        {
            PlayJumpSound();
            _animator.SetTrigger("takeOf");
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
        }

        if (_isGrounded)
            _animator.SetBool("isJumping", false);
        else
            _animator.SetBool("isJumping", true);

        _isTouchingFronts = Physics2D.OverlapCircle(_frontsCheck.position, _checkRadius, _whatIsGground);
        if (_isTouchingFronts == true && _isGrounded == false && input != 0)
            _wallSliding = true;
        else
            _wallSliding = false;

        if (_wallSliding)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Clamp(_rigidbody2D.velocity.y, -_wallSlidingSpeed, float.MaxValue));

        if ((Input.GetAxisRaw("Vertical") != 0) && _wallSliding == true)
        {
            PlayJumpSound();
            _wallJumping = true;
            StartCoroutine(SetWallJumpingToFalse());
        }

        if (_wallJumping)
            _rigidbody2D.velocity = new Vector2(_xWallForce * -input, _yWallForce);
    }

    public void Land()
    {
        Vector2 pos = new Vector2(_groundCheck.position.x, _groundCheck.position.y + 1);
        Instantiate(_dropEffect, pos, Quaternion.identity);
    }

    private void PlayJumpSound()
    {
        _audioSource.clip = _jumpSound;
        _audioSource.Play();
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _facingRight = !_facingRight;
    }

    private IEnumerator SetWallJumpingToFalse()
    {
        float elapsedTime = _wallJumpTime;
        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;
        }

        _wallJumping = false;
    }
}
