using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponChest_sc : interactable_sc
{



    public override void Interact()
    {
        base.Interact();

        Debug.Log("Interacted with" + promtMessage);
    }

}
