using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator), typeof(BoxCollider2D))]

public class DoorAnnunciator : MonoBehaviour
{
    private Animator _animator = null;
    private SoundSystem _soundSystem = null;

    private const string DoorOpenConditionName = "isDoorOpened";

    private void Start()
    {
        _soundSystem = Camera.main.GetComponentInChildren<SoundSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            if (_soundSystem != null)
            {
                _soundSystem.SetVolume(0.0f);
                _soundSystem.PlayLoop(SoundSystem.AudioType.DoorAlarm);
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
            if (_soundSystem != null)
                _soundSystem.PingPongVolume();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            if (_soundSystem != null)
                _soundSystem.Stop();

            if (_animator != null)
                _animator.SetBool(DoorOpenConditionName, false);
        }
    }

}
