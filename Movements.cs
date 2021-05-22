using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movements : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Animator _animator;
    private Rigidbody2D _rigidbody = null;
    private SpriteRenderer _spriteRenderer = null;
    bool _facingRight = true;
    bool _isJumped = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (TryGetComponent(out Rigidbody2D rigidbody))
        {
            _rigidbody = rigidbody;
        }
    }

    private void Update()
    {
        if (_animator == null || _spriteRenderer == null)
            return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            if (!_facingRight)
            {
                _facingRight = true;
                _spriteRenderer.flipX = false;
            }
            _animator.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
            if (_facingRight)
            {
                _facingRight = false;
                _spriteRenderer.flipX = true;
            }
            _animator.SetBool("run", true);
        }
        else
        {
            transform.Translate(0, 0, 0);
            if (_animator.GetBool("run"))
                _animator.SetBool("run", false);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_animator == null)
            return;
        if (_animator.GetBool("jump"))
        {
            _animator.SetBool("jump", false);
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) 
            && (collision.gameObject.GetComponentsInParent<Ground>().Length > 0 || collision.gameObject.GetComponentsInParent<Monster>().Length > 0) 
            && !_isJumped)
        {
            _isJumped = true;
            _rigidbody.AddForce(Vector2.up, ForceMode2D.Force);
            _animator.SetBool("jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isJumped)
            _isJumped = false;
    }
}
