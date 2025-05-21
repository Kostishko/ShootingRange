using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;




// references on assets https://ilay-rubinchik.itch.io/low-poly-targe
public class gameManager_sc : MonoBehaviour
{

    public GameObject target;
    public GameObject AreasParent;
    public List<Transform> spawnAreas; // Assign spawn areas in the Inspector
    public int maxTargets = 6; // Maximum number of targets alive at once
    public float gameDuration = 60f; // Duration of the gameplay in seconds
    public float elapsedTime = 0f; // Time elapsed since the game started
    public float spawnDelay = 1f; // Delay between target spawns
    public GameObject character;

    public int score = 0; // Player's score 

    public event EventHandler<GameState> gameStateChanged;
    public event EventHandler runEnded;


    public enum GameState
    {
        MainMenu,        
        Gameplay,
        Waiting,
        Inventary
    }

    [SerializeField]
    private List<GameObject> activeTargets = new List<GameObject>();


    public static GameState currentGameState;

    public void Start()
    {

        spawnAreas = new List<Transform>(AreasParent.transform.childCount);
        for (int i = 0; i < AreasParent.transform.childCount; i++)
        {
            spawnAreas.Add(AreasParent.transform.GetChild(i));
        }
    }

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
        gameStateChanged?.Invoke(this, newState); // Notify subscribers of the state change
        Debug.Log("Game state changed to: " + currentGameState);
    }

    public void StartGame()
    {
        SetGameState(GameState.Gameplay);
        StartCoroutine(GameplayCoroutine());
        score = 0; // Reset score when starting a new game
    }
    private IEnumerator GameplayCoroutine()
    {
        elapsedTime = 0f;
        float spawnDelayTimer = 0f;


        while (elapsedTime < gameDuration)
        {
            if (activeTargets.Count < maxTargets && spawnDelayTimer <=0)
            {
                SpawnTarget();
                spawnDelayTimer = spawnDelay;
            }
            else
            {
                spawnDelayTimer -= Time.deltaTime;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy all remaining targets after the game ends
        foreach (var target in activeTargets)
        {
            if (target != null)
            {
                Destroy(target);
            }
        }
        activeTargets.Clear();
        runEnded?.Invoke(this, EventArgs.Empty); // Notify subscribers that the game has ended
        SetGameState(GameState.MainMenu);
        UnityEngine.Cursor.lockState = CursorLockMode.None;

    }

    private void SpawnTarget()
    {
        // Choose a random spawn area
        Transform spawnArea = spawnAreas[UnityEngine.Random.Range(0, spawnAreas.Count)];



        GameObject newTarget = Instantiate(target);
        newTarget.transform.position = spawnArea.position;
        newTarget.GetComponent<target_sc>().character = character;
        activeTargets.Add(newTarget);

        // Remove the target from the list when it is destroyed
        newTarget.GetComponent<target_sc>().OnDestroyed += (sender, args) => activeTargets.Remove(newTarget);
        newTarget.GetComponent<target_sc>().OnDestroyed += (sender, args) =>
        {
            score += 10; // Increment score when a target is destroyed
        };


    }





}
