using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderEffect;
    private void Start()
    {
        DataManager.Load();
        _sliderMusic.value = DataManager.data.musicSound;
        _sliderEffect.value = DataManager.data.effectSound;
        
        if (DataManager.data.musicSound == 0)
        {
            _audioMixer.SetFloat("MusicVolume", -80);
        }
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(DataManager.data.musicSound) * 20);
        
        
        if (DataManager.data.effectSound == 0)
        {
            _audioMixer.SetFloat("EffectVolume", -80);
        }
        _audioMixer.SetFloat("EffectVolume", Mathf.Log10(DataManager.data.effectSound) * 20);
    }


    public void SetMusicVolume(float value)
    {
        DataManager.Load();
        DataManager.data.musicSound = value;
        
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        if (value == 0)
        {
            _audioMixer.SetFloat("MusicVolume", -80);
        }
    }
    
    public void SetEffectVolume(float value)
    {
        DataManager.Load();
        DataManager.data.effectSound = value;
        
        _audioMixer.SetFloat("EffectVolume", Mathf.Log10(value) * 20);
        if (value == 0)
        {
            _audioMixer.SetFloat("EffectVolume", -80);
        }
    }
}
