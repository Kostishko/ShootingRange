using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;




public class gameplayOverlayUI_sc : MonoBehaviour
{

    public VisualElement root;
    public Label timer;
    public Label score;

    public gameManager_sc gameManager; // Reference to the GameManager script

    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        timer = root.Q<Label>("Timer");
        score = root.Q<Label>("Score");
        timer.visible = false;
        score.visible = false;
    }


    public void Update()
    {
        if(gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay)
        {
            timer.visible = true;
            score.visible = true;
            timer.text = (60f-gameManager.elapsedTime).ToString("F2");
            score.text = gameManager.score.ToString();
        }
        else
        {
            timer.visible = false;
            score.visible = false;
        }
    }




}
