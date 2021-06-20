using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundSystem : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] private float _speed;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _doorAlarmSound;

    public enum AudioType
    {
        Coin,
        MonsterSpawn,
        MonsterKilled,
        HeroJump,
        HeroRun,
        DoorAlarm
    }

    private AudioSource _audioSource = null;
    private Dictionary<AudioType, AudioClip> _audioMap = null;

    private void Awake()
    {
        if (TryGetComponent(out AudioSource audioSource))
        {
            _audioSource = audioSource;
        }

        AddAudioAssetsToMap();
    }

    private void AddAudioAssetsToMap()
    {
        _audioMap = new Dictionary<AudioType, AudioClip>
        {
            { AudioType.Coin, _coinSound },
            { AudioType.DoorAlarm, _doorAlarmSound }
        };
    }

    public void PlaySound(AudioType type)
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(_audioMap[type]);
    }

    public void PlayLoop(AudioType type)
    {
        if (_audioSource != null)
        {
            _audioSource.clip = _audioMap[type];
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    public void SetVolume(float value)
    {
        if (_audioSource != null)
            _audioSource.volume = value;
    }

    public void Stop()
    {
        if (_audioSource != null)
            _audioSource.Stop();
    }

    /// <summary> Fade in and fade out smoothly, by changing the volume from 0 to 1 </summary>
    public void PingPongVolume()
    {
        if (_audioSource != null)
            _audioSource.volume = Mathf.PingPong(Time.time, 1.0f);
    }
}
