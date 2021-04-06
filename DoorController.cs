using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class DoorController : MonoBehaviour
{
    [SerializeField] [Range(0, 3)] private float _speed;
    private AudioSource _audioSource;
    private Animator _animator;

    float targetValue = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.0f;
        _audioSource.Play();

        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
            _animator.SetBool("isDoorOpened", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //PingPong returns a value that will increment and decrement between the value 0 and length
        //_audioSource.volume = Mathf.PingPong(Time.time, 1.0f);

        ChangeVolume();

        Debug.Log("volume = " + _audioSource.volume);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _audioSource.Stop();
        _animator.SetBool("isDoorOpened", false);
    }

    private void ChangeVolume()
    {
        if (_audioSource.volume == 0)
        {
            targetValue = 1.0f;
        }
        else if (_audioSource.volume == 1)
        {
            targetValue = 0.0f;
        }

        _audioSource.volume = Vector2.MoveTowards(new Vector2(_audioSource.volume, 0.0f), new Vector2(targetValue, 0.0f), _speed * Time.deltaTime).x;
    }

}
