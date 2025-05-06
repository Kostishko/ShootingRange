using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// references on assets https://ilay-rubinchik.itch.io/low-poly-targe
public class gameManager_sc : MonoBehaviour
{
  
    public enum GameState
    {
        MainMenu,        
        Gameplay,
        Waiting,
        Inventary
    }

    public static GameState currentGameState;

    public void Update()
    {
        
        switch(currentGameState)
        {
            case GameState.MainMenu:
                // Handle Main Menu logic
                break;
            case GameState.Gameplay:
                // Handle Gameplay logic
                break;
            case GameState.Waiting:
                // Handle Inventary logic
                break;
        }
    }

    public void SetGameState(GameState newState)
    {
        currentGameState = newState;
        Debug.Log("Game state changed to: " + currentGameState);
    }






}
