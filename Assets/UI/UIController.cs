using System.Collections;
using System.Collections.Generic;
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
    public Button HelpButton;
    public Button CreatorsButton;
    public Button SettingsButton;
    public Button ExitButton;

    //settings menu
    public VisualElement SettingsPanel;
    public Button SettingsBackButton;

    //Help menu
    public VisualElement HelpPanel;
    public Button RecordsBackButton;

    //Creators menu
    public VisualElement CreatorsPanel;
    public Button CreatorsBackButton;

    //overlay elements
    public VisualElement overlayRoot;

    //inventary elements
    public VisualElement inventaryRoot;

    //promted elements
    public VisualElement promptMessage;
    public Label promtLabel;

    //Character
    public characterInteraction_sc characterInteraction;


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
        HelpButton = root.Q<Button>("HelpButton");
        CreatorsButton = root.Q<Button>("CreatorsButton");
        SettingsButton = root.Q<Button>("SettingsButton");
        ExitButton = root.Q<Button>("ExitButton");
        MainMenuPanel.RemoveFromClassList("hiddenLeft");
        //MainMenuPanel.AddToClassList("shownPanel");

        NewGameButton.clicked += () =>
        {
            gameManager.SetGameState(gameManager_sc.GameState.Waiting);
            MainMenuPanel.AddToClassList("hiddenLeft");            
            //MainMenuPanel.RemoveFromClassList("shownPanel");
            overlayRoot.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        };

        HelpButton.clicked += () =>
        {
            MainMenuPanel.AddToClassList("hiddenLeft");
            //MainMenuPanel.RemoveFromClassList("shownPanel");
            HelpPanel.RemoveFromClassList("hiddenRight");
            //RecordsPanel.AddToClassList("shownPanel");

        };

        CreatorsButton.clicked += () =>
        {
            MainMenuPanel.AddToClassList("hiddenLeft");
            //MainMenuPanel.RemoveFromClassList("shownPanel");
           CreatorsPanel.RemoveFromClassList("hiddenRight");
            //CreatorsPanel.AddToClassList("shownPanel");

        };

        SettingsButton.clicked += () =>
        {
            MainMenuPanel.AddToClassList("hiddenLeft");
            //MainMenuPanel.RemoveFromClassList("shownPanel");
            SettingsPanel.RemoveFromClassList("hiddenTop");
            //SettingsPanel.AddToClassList("shownPanel");

        };

        ExitButton.clickable.clicked += () =>
        {
            Application.Quit();
        };

        SettingsPanel = root.Q("Setting");
        SettingsBackButton = root.Q<Button>("SettingsBackButton");
        SettingsBackButton.clicked += () =>
        {
            SettingsPanel.AddToClassList("hiddenTop");
            //SettingsPanel.RemoveFromClassList("shownPanel");
            MainMenuPanel.RemoveFromClassList("hiddenLeft");
            //MainMenuPanel.AddToClassList("shownPanel");
        };

        HelpPanel = root.Q("HelpPanel");
        RecordsBackButton = root.Q<Button>("RecordsBackButton");
        RecordsBackButton.clicked += () =>
        {
            HelpPanel.AddToClassList("hiddenRight");
            //RecordsPanel.RemoveFromClassList("shownPanel");
            MainMenuPanel.RemoveFromClassList("hiddenLeft");
            //MainMenuPanel.AddToClassList("shownPanel");
        };

        CreatorsPanel = root.Q("CreatorsPanel");
        CreatorsBackButton = root.Q<Button>("CreatorsBackButton");
        CreatorsBackButton.clicked += () =>
        {
            CreatorsPanel.AddToClassList("hiddenRight");
            //CreatorsPanel.RemoveFromClassList("shownPanel");
            MainMenuPanel.RemoveFromClassList("hiddenLeft");
            //MainMenuPanel.AddToClassList("shownPanel");
        };


        //overlay elements initialisation
        overlayRoot = root.Q("GameplayOverlay");
        overlayRoot.style.display = DisplayStyle.None;
        //inventaryRoot = root.Q("InventaryRoot");

        //prompt
        promptMessage = root.Q("PromtBox");
        promtLabel = promptMessage.Q<Label>("PromtLabel");
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
                MainMenuPanel.RemoveFromClassList("hiddenLeft");
                UnityEngine.Cursor.lockState = CursorLockMode.None;
            }            
        }
        


        //promt message logic
        if(characterInteraction.isInteracting)
        {
            promptMessage.style.visibility = Visibility.Visible;
            promtLabel.text = characterInteraction.detectHit.collider.GetComponent<interactable_sc>().promtMessage;
            Vector3 screenPos = characterInteraction.cam.WorldToScreenPoint(characterInteraction.detectHit.collider.transform.position);
            promptMessage.style.left = screenPos.x + promptMessage.layout.width/2;
            promptMessage.style.top = Screen.height - screenPos.y - 100;
        }
        else
        {
            promptMessage.style.visibility = Visibility.Hidden;
        }

    }

    public void Awake()
    {
        // Set the initial game state to MainMenu
        gameManager.SetGameState(gameManager_sc.GameState.MainMenu);
    }

    public void OpenInventoryPanel()
    {

    }

    public void CloseInventoryPanel()
    {

    }

}
