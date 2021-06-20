using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Movements))]

public class Movements : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody = null;
    private bool _isCollisionWithGround = false;
    private bool _isCollisionWithMonster = false;
    private bool IsAlreadyJumped = false;

    private void Start()
    {
        if (TryGetComponent(out Rigidbody2D rigidbody))
        {
            _rigidbody = rigidbody;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(Vector3.zero);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isCollisionWithGround = collision.gameObject.GetComponentsInParent<Ground>().Length > 0;

        if (!_isCollisionWithGround)
            _isCollisionWithMonster = collision.gameObject.GetComponentsInParent<Monster>().Length > 0;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CanJump())
        {
            IsAlreadyJumped = true;
            _rigidbody.AddForce(Vector2.up, ForceMode2D.Force);
            StartCoroutine(ResetJump());
        }
    }

    public bool CanJump ()
    {
        return (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) 
            && (_isCollisionWithGround || _isCollisionWithMonster) 
            && !IsAlreadyJumped;  
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(1.0f);
        if (IsAlreadyJumped)
            IsAlreadyJumped = false;
    }
}
