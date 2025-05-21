using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager_sc : MonoBehaviour
{
  
    public static SoundFXManager_sc instance;

    [SerializeField] private AudioSource AudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }        
    }

    public void PlaySoundFXClip(AudioClip clip, Transform FXTransform, float volume)
    {
        //instantinate new audio clip
        AudioSource newSource = Instantiate(AudioSource, FXTransform.position, Quaternion.identity);
        newSource.clip = clip;
        newSource.volume = volume;
        newSource.Play();
        float length = clip.length;
        Destroy(newSource.gameObject, length);

    }

}


