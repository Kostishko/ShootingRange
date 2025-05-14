using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGameButton_sc : interactable_sc
{
    public gameManager_sc gameManager;

    public GameObject nonActive;
    public GameObject active;

    private void Start()
    {
        gameManager.gameStateChanged += ActiveButton;

    }

    public override void Interact()
    {
        base.Interact();
        if(gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
        {
            gameManager.StartGame();
        }
    }

    public void ActiveButton(object sender, gameManager_sc.GameState e)
    {
        if (e == gameManager_sc.GameState.Gameplay)
        {
            nonActive.SetActive(false);
            active.SetActive(true);
        }
        else
        {
            nonActive.SetActive(true);
            active.SetActive(false);
        }
    }


}
    

