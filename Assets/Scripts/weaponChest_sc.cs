using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponChest_sc : interactable_sc
{



    protected override void Interact()
    {
        base.Interact();
        //open chest
        //play animation
        //play sound
        //give weapon
        Debug.Log("Interacted with" + promtMessage);
    }

}
