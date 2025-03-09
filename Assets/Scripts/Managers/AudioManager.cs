using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public bool isSFXMuted = false;
    public bool isMusicMuted = false;

    void Awake()
    {
        audioMixer.SetFloat("SFXVol", 0f);
        audioMixer.SetFloat("MusicVol", 0f);
    }

    public void SwitchSFXVolume()
    {
        if (!isSFXMuted) audioMixer.SetFloat("SFXVol", -80f);
        else audioMixer.SetFloat("SFXVol", 0f);

        isSFXMuted = !isSFXMuted;
    }

    public void SwitchMusicVolume()
    {
        if (!isMusicMuted) audioMixer.SetFloat("MusicVol", -80f);
        else audioMixer.SetFloat("MusicVol", 0f);

        isMusicMuted = !isMusicMuted;
    }


}
