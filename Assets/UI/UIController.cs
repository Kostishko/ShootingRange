using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public gameManager_sc gameManager; // Reference to the GameManager script

    //major root element
    public VisualElement root;

    //menu elements
    //main Menu
    public VisualElement menuRoot;
    public VisualElement MainMenuPanel;
    public Button NewGameButton;
    public Button RecordsButton;
    public Button SettingsButton;

    //settings menu
    public VisualElement SettingsPanel;

    //records menu
    public VisualElement RecordsPanel;

    //overlay elements
    public VisualElement overlayRoot;

    //inventary elements
    public VisualElement inventaryRoot;


    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //menu elements initialisation
        menuRoot = root.Q("Canvas");
        menuRoot.style.display = DisplayStyle.Flex;

        //main menu
        MainMenuPanel = root.Q("MainMenuPanel");
        MainMenuPanel.style.display = DisplayStyle.Flex;
        NewGameButton = root.Q<Button>("NewGameButton");
        RecordsButton = root.Q<Button>("RecordsButton");
        SettingsButton = root.Q<Button>("SettingsButton");

        NewGameButton.clicked += () =>
        {
            gameManager.SetGameState(gameManager_sc.GameState.Gameplay);
            menuRoot.style.display = DisplayStyle.None;
            overlayRoot.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        };

        RecordsButton.clicked += () =>
        {
            MainMenuPanel.style.display = DisplayStyle.None;
            RecordsPanel.style.display = DisplayStyle.Flex;
        };

        SettingsButton.clicked += () =>
        {
            MainMenuPanel.style.display = DisplayStyle.None;
            SettingsPanel.style.display = DisplayStyle.Flex;
        };

        SettingsPanel = root.Q("Setting");
        SettingsPanel.style.display = DisplayStyle.None;
        RecordsPanel = root.Q("Records");
        RecordsPanel.style.display = DisplayStyle.None;

        //overlay elements initialisation
        overlayRoot = root.Q("GameplayOverlay");
        overlayRoot.style.display = DisplayStyle.None;
        //inventaryRoot = root.Q("InventaryRoot");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay || 
                gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting ||
                gameManager_sc.currentGameState == gameManager_sc.GameState.Inventary)
            {
                gameManager.SetGameState(gameManager_sc.GameState.MainMenu);
                overlayRoot.style.display = DisplayStyle.None;
                menuRoot.style.display = DisplayStyle.Flex;
                MainMenuPanel.style.display = DisplayStyle.Flex;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
            }            
        }
        else
        {
           Application.Quit();
        }
    }


}
