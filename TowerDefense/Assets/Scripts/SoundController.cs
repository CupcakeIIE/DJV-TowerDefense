using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        InitSlider(mainSlider, "MainVolume");
        InitSlider(musicSlider, "MusicVolume");
        InitSlider(sfxSlider, "SFXVolume");

        mainSlider.onValueChanged.AddListener(value => SetVolume("MainVolume", value));
        musicSlider.onValueChanged.AddListener(value => SetVolume("MusicVolume", value));
        sfxSlider.onValueChanged.AddListener(value => SetVolume("SFXVolume", value));
    }

    void InitSlider(Slider slider, string exposedParam)
    {
        if (audioMixer.GetFloat(exposedParam, out float volumeDb))
        {
            float linearValue = Mathf.Exp(volumeDb / 20f);
            slider.value = linearValue;
        }
    }

    
    void SetVolume(string exposedParam, float linearValue)
    {
        // Convertit la valeur linéaire (0 à 1) en décibels (dB)
        float volumeDb = Mathf.Log10(Mathf.Clamp(linearValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(exposedParam, volumeDb);
    }
}
