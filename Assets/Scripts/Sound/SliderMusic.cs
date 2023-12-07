using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class SliderMusic : MonoBehaviour
{
    [SerializeField] private Slider _sliderMusic;
    private AudioMixerManager _audioMixerManager;
    
    private void OnEnable()
    {
        _audioMixerManager = FindObjectOfType<AudioMixerManager>();
        DataManager.Load();
        _sliderMusic.value = DataManager.data.musicSound;
    }

    public void SetVolume(float volume)
    {
        _audioMixerManager.SetMusicVolume(volume);
    }
}
