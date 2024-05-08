using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip buttonClick;
    public AudioClip stone_Hit;
    public AudioClip dead_Sound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ButtonClick()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
    }

    public void StoneHit(AudioSource stone_Audio)
    {
        stone_Audio.clip = stone_Hit;
        stone_Audio.Play();
    }

    public void Dead()
    {
        audioSource.clip = dead_Sound;
        audioSource.Play();
    }
}
