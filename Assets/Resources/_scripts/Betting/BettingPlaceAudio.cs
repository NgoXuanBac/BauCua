using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingPlaceAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _mouseDownSound;
    [SerializeField] private AudioClip _mouseUpSound;
    [SerializeField] private AudioSource _audioSource;

    public void PlayDownSound()
    {
        _audioSource.PlayOneShot(_mouseDownSound);
    }
    public void PlayUpSound()
    {
        _audioSource.PlayOneShot(_mouseUpSound);
    }
    
}
