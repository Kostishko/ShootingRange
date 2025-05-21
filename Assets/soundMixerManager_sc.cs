using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundMixerManager_sc : MonoBehaviour
{

    public AudioMixer audioMixer;


    public void SetLevel(float sliderValue)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelMusic(float sliderValue)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelSFX(float sliderValue)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(sliderValue) * 20);
    }


}
