using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]

public class DoorAnnunciator : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] private float _speed;

    private AudioSource _audioSource;
    private Animator _animator;
    float targetValue = 1.0f;
    const string DOOR_OPEN_TAG = "isDoorOpened";

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Hero>() != null)
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.0f;
            _audioSource.Play();

            if (TryGetComponent(out Animator animator))
            {
                _animator = animator;
                _animator.SetBool(DOOR_OPEN_TAG, true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>() != null)
        {
            ChangeVolume();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>() != null)
        {
            _audioSource.Stop();
            _animator.SetBool(DOOR_OPEN_TAG, false);
        }
    }

    /// <summary> Fade in and fade out smoothly, by changing the volume from 0 to 1 </summary>
    private void ChangeVolume()
    {
        /* 
        * Для периодического изменения громкости наиболее подходит Mathf.PingPong:
        * // PingPong returns a value that will increment and decrement between the value 0 and length
        * _audioSource.volume = Mathf.PingPong(Time.time, 1.0f);
        * Но поскольку в таске написано "Перед выполнением изучите Mathf.MoveTowards", то попробуем использовать ее
        * и получим такой же результат как и при использовании Mathf.PingPong:
        */

        if (_audioSource.volume == 0)
        {
            targetValue = 1.0f;
        }
        else if (_audioSource.volume == 1)
        {
            targetValue = 0.0f;
        }

        _audioSource.volume = Vector2.MoveTowards(new Vector2(_audioSource.volume, 0.0f)
                                                , new Vector2(targetValue, 0.0f)
                                                , _speed * Time.deltaTime).x;
    }

}
