using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Movements))]

public class Movements : MonoBehaviour
{
    [SerializeField] private float _speed;

    public bool IsAlreadyJumped { get; private set; } = false;

    private Rigidbody2D _rigidbody = null;
    private bool _isCollisionWithGround = false;
    private bool _isCollisionWithMonster = false;

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
            transform.Translate(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isCollisionWithGround = collision.gameObject.GetComponentsInParent<Ground>().Length > 0;

        if (!_isCollisionWithGround)
            _isCollisionWithMonster = collision.gameObject.GetComponentsInParent<Monster>().Length > 0;

        if (IsAlreadyJumped)
            IsAlreadyJumped = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CanJump())
        {
            IsAlreadyJumped = true;
            _rigidbody.AddForce(Vector2.up, ForceMode2D.Force);
        }
    }

    public bool CanJump ()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            && (_isCollisionWithGround || _isCollisionWithMonster)
            && !IsAlreadyJumped)
        {
            return true;
        }
        return false;
    }

}
