using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CoinAnimator : MonoBehaviour
{
    private Animator _animator = null;
    private bool _isCollisionWithHero = false;
    private const string _catchTrigger = "Catched";
    private const float _coinUpSpeed = 5.0f;

    private void Start()
    {
        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }
    }

    private void Update()
    {
        if (_isCollisionWithHero)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, _coinUpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_animator == null)
            return;

        _isCollisionWithHero = collider.gameObject.GetComponentInChildren<Hero>() != null;
        if (_isCollisionWithHero)
        {
            _animator.SetTrigger(_catchTrigger);
        }
    }
}
