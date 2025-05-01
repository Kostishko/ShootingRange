using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Object that can be interactable
/// </summary>
public abstract class interactable_sc : MonoBehaviour
{

    public string promtMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        
    }    
}
