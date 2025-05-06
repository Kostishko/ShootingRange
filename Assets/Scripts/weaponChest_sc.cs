using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponChest_sc : interactable_sc
{



    public override void Interact()
    {
        base.Interact();
        if(gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
        {

        }


        Debug.Log("Interacted with" + promtMessage);
    }

}
