using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]

public class DoorAnnunciator : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] private float _speed;

    private AudioSource _audioSource = null;
    private Animator _animator = null;
    private float targetValue = 1.0f;

    private const string DoorOpenConditionName = "isDoorOpened";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            if (TryGetComponent(out AudioSource audioSource))
            {
                _audioSource = audioSource;
                _audioSource.volume = 0.0f;
                _audioSource.Play();
            }

            if (TryGetComponent(out Animator animator))
            {
                _animator = animator;
                _animator.SetBool(DoorOpenConditionName, true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            ChangeVolume();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            if (_audioSource != null)
                _audioSource.Stop();

            if (_animator != null)
                _animator.SetBool(DoorOpenConditionName, false);
        }
    }

    /// <summary> Fade in and fade out smoothly, by changing the volume from 0 to 1 </summary>
    private void ChangeVolume()
    {
        if (_audioSource == null)
            return;

        //_audioSource.volume = Mathf.PingPong(Time.time, 1.0f);

        if (_audioSource.volume == 0)
        {
            targetValue = 1.0f;
        }
        else if (_audioSource.volume == 1)
        {
            targetValue = 0.0f;
        }

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetValue, _speed * Time.deltaTime);
    }

}
