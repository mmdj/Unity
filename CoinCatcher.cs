using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CircleCollider2D))]

public class CoinCatcher : MonoBehaviour
{
    private Animator _animator = null;
    private Collider2D _collider = null;
    private SoundSystem _soundSystem = null;
    private CoinsSpawner _coinsSpawner = null;
    private bool _isCollisionWithHero = false;

    private const string _catchTrigger = "Catched";
    private const float _coinUpSpeed = 5.0f;


    private void Start()
    {
        if (TryGetComponent (out Animator animator))
        {
            _animator = animator;
        }

        if (TryGetComponent(out CircleCollider2D collider))
        {
            _collider = collider;
        }

        _soundSystem = Camera.main.GetComponentInChildren<SoundSystem>();
        _coinsSpawner = transform.parent.GetComponentInParent<CoinsSpawner>();
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
            if (_collider != null)
                _collider.enabled = false;

            _animator.SetTrigger(_catchTrigger);

            if (_soundSystem != null)
                _soundSystem.PlaySound(SoundSystem.AudioType.Coin);
            
            if (_coinsSpawner != null)
                _coinsSpawner.SpawnNextCoin();
        }
    }
}
