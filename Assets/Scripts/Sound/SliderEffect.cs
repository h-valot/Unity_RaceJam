using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class SliderEffect : MonoBehaviour
{
    [SerializeField] private Slider _sliderEffect;
    private AudioMixerManager _audioMixerManager;


    private void OnEnable()
    {
        _audioMixerManager = FindObjectOfType<AudioMixerManager>();
        DataManager.Load();
        _sliderEffect.value = DataManager.data.effectSound;
    }

    public void SetVolume(float volume)
    {
        _audioMixerManager.SetEffectVolume(volume);
    }
}
