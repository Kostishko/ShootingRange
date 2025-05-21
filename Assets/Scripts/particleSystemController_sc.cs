using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSystemController_sc : MonoBehaviour
{
    public ParticleSystem particleSystemShot;
    public ParticleSystem particleSystemReload;

    
    public void PlayShotParticleSystem()
    {
        if (particleSystemShot != null)
        {
            particleSystemShot.Play();
        }
        else
        {
            Debug.LogWarning("Particle system for shot is not assigned.");
        }
    }

    public void PlayReloadParticleSystem()
    {
        if (particleSystemReload != null)
        {
            particleSystemReload.Play();
        }
        else
        {
            Debug.LogWarning("Particle system for reload is not assigned.");
        }
    }

}
