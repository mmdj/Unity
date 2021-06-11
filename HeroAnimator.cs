using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Hero))]

public class HeroAnimator : MonoBehaviour
{
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private Movements _movements = null;

    private bool _facingRight = true;

    private const string _animatorConditionRun = "run";
    private const string _animatorConditionJump = "jump";

    private void Start()
    {
        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }

        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            _spriteRenderer = spriteRenderer;
        }

        _movements = gameObject.GetComponentInParent<Movements>();
    }

    private void Update()
    {
        if (_animator == null || _spriteRenderer == null)
            return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_facingRight)
            {
                _facingRight = true;
                _spriteRenderer.flipX = false;
            }
            _animator.SetBool(_animatorConditionRun, true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_facingRight)
            {
                _facingRight = false;
                _spriteRenderer.flipX = true;
            }
            _animator.SetBool(_animatorConditionRun, true);
        }
        else
        {
            if (_animator.GetBool(_animatorConditionRun))
            {
                _animator.SetBool(_animatorConditionRun, false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_animator == null || _movements == null)
            return;

        if (_movements.CanJump() && !_animator.GetBool(_animatorConditionJump))
        {
            _animator.SetBool(_animatorConditionJump, true);
        }
        else if (_animator.GetBool(_animatorConditionJump))
        {
            _animator.SetBool(_animatorConditionJump, false);
        }
    }
}
