using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponChest_sc : interactable_sc
{
    //somwhere I had to put this in different way. 
    public weaponChestUIController_sc weaponChestUIController;


    public override void Interact()
    {
        base.Interact();
        if (gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
        {
            weaponChestUIController.ShowWeaponChestUI();
        }
        else
        {
            //some code that show error message to player. 
            // in a good worl it should be a pop up message
        }
        Debug.Log("Interacted with" + promtMessage);
    }

}
