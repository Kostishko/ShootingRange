using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;




public class gameplayOverlayUI_sc : MonoBehaviour
{

    public VisualElement root;

    //panel run end
    public VisualElement RunEnd;
    public TextField nameInput;
    public Button submitButton; 
    public Label scoreAtCongrats; 

    //overlay
    public Label timer;
    public Label score;

    public gameManager_sc gameManager; // Reference to the GameManager script

    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //run ended panel 
        RunEnd = root.Q("RunEnd");
        nameInput = RunEnd.Q<TextField>("NameInput");
        nameInput.SetEnabled(false);
        submitButton = RunEnd.Q<Button>("RunEndButton");
        scoreAtCongrats = RunEnd.Q<Label>("ScoreAtCongrats");
        RunEnd.AddToClassList("hiddenLeft");
        submitButton.clicked += () =>
        {
            //shoud be logic of remebering the score and player name

            //finishing the run
            RunEnd.AddToClassList("hiddenLeft");
            nameInput.SetEnabled(false);
            gameManager.SetGameState(gameManager_sc.GameState.Waiting);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        };

        //Overlay
        timer = root.Q<Label>("Timer");
        score = root.Q<Label>("Score");
        timer.visible = false;
        score.visible = false;

        gameManager.runEnded += ShowRunEnd;
    }


    public void Update()
    {
        if(gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay)
        {
            timer.visible = true;
            score.visible = true;
            timer.text = (60f-gameManager.elapsedTime).ToString("F2");
            score.text ="Score: " + gameManager.score.ToString();
            scoreAtCongrats.text = "Score: " + gameManager.score.ToString();
        }
        else
        {
            timer.visible = false;
            score.visible = false;
        }
    }

    public void ShowRunEnd(object sender, EventArgs a)
    {
        nameInput.SetEnabled(true);
        RunEnd.RemoveFromClassList("hiddenLeft");        
    }




}
