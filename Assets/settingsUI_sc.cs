using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class settingsUI_sc : MonoBehaviour
{
    private VisualElement rootElement;

    private Slider masterVolumeSlider;
    private Slider soundFXXSlider;
    private Slider musicVolumeSlider;

    public soundMixerManager_sc soundMixerManager; // Reference to the sound mixer manager

    public void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        // Find the sliders in the UI
        masterVolumeSlider = rootElement.Q<Slider>("MasterSlider");
        soundFXXSlider = rootElement.Q<Slider>("SoundSlider");
        musicVolumeSlider = rootElement.Q<Slider>("MusicSlider");

        masterVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            // Call the method to set the master volume
            soundMixerManager.SetLevel(evt.newValue);
        });

        soundFXXSlider.RegisterValueChangedCallback(evt =>
        {
            // Call the method to set the sound effects volume
            soundMixerManager.SetLevelSFX(evt.newValue);
        });
        musicVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            // Call the method to set the music volume
            soundMixerManager.SetLevelMusic(evt.newValue);
        });

        masterVolumeSlider.value = 0.5f;

        soundFXXSlider.value = 0.5f;
        musicVolumeSlider.value = 0.5f;
    }


}
