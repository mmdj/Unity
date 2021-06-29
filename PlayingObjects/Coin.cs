using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]

public class Coin : MonoBehaviour
{
    private SoundSystem _soundSystem = null;
    private Collider2D _collider = null;

    private void Start()
    {
        _soundSystem = Camera.main.GetComponentInChildren<SoundSystem>();

        if (TryGetComponent(out CircleCollider2D collider))
        {
            _collider = collider;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_soundSystem != null)
            _soundSystem.PlaySound(SoundSystem.AudioType.Coin);

        if (_collider != null)
            _collider.enabled = false;
    }
}
